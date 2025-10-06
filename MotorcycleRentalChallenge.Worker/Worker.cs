using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Events;
using MotorcycleRentalChallenge.Infrastructure.Data;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace MotorcycleRentalChallenge.Worker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ConnectionFactory _factory;

        private IConnection _connection;
        private IChannel _channel;        

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;

            _factory = new ConnectionFactory() { HostName = "rabbitmq", AutomaticRecoveryEnabled = true};
        }

        private async Task ConnectToRabbitMq(CancellationToken stoppingToken)
        {
            int retryCount = 0;
            const int maxRetries = 5;

            while(!stoppingToken.IsCancellationRequested && _connection == null)
            {
                try
                {
                    _logger.LogInformation("Attempting to connect to RabbitMQ (Attempt {count}/{max})", retryCount + 1, maxRetries);

                    _connection = await _factory.CreateConnectionAsync(stoppingToken);
                    _channel = await _connection.CreateChannelAsync();

                    await _channel.QueueDeclareAsync(
                        queue: "motorcycles-registered",
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null
                    );

                    _logger.LogInformation("RabbitMQ connection and queue setup successful.");
                    return;
                }
                catch (BrokerUnreachableException ex)
                {
                    retryCount++;                    

                    if (retryCount >= maxRetries)
                    {
                        _logger.LogError($"Maximum retry limit ({maxRetries}) reached. Finishing service.");
                        throw;
                    }                    
                    await Task.Delay(5000, stoppingToken);                    
                }
                catch (Exception)
                {

                    throw;
                }
            }            
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ConnectToRabbitMq(stoppingToken);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (ch, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    await ConsumeMotorcycleEvent(message);

                    await _channel.BasicAckAsync(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error consuming message.");
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, true);
                }
                
            };

            await _channel.BasicConsumeAsync(
                queue: "motorcycles-registered", 
                autoAck: false, 
                consumer: consumer);


            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task  ConsumeMotorcycleEvent(string message)
        {
            try
            {
                var motorcycleEvent = JsonSerializer.Deserialize<MotorcycleRegisteredEvent>(message);
                if (motorcycleEvent != null && motorcycleEvent.Year == 2024)
                {

                    var notification = new MotorcycleNotification(
                        motorcycleEvent.Id,
                        motorcycleEvent.Year,
                        motorcycleEvent.Plate);
                    
                    await using(var scope = _serviceScopeFactory.CreateAsyncScope())
                    {
                        var bd = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                        await bd.AddAsync<MotorcycleNotification>(notification);
                        await bd.SaveChangesAsync();
                    }                    

                    _logger.LogInformation($"Motorcycle year 2024 and with plate {motorcycleEvent.Plate} was registered.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error consuming message.");
                throw;
            }
        }

        public override void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            base.Dispose();
        }
    }
}
