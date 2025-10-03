using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MotorcycleRentalChallenge.Infrastructure.Messaging
{
    public class RabbitMqService : IMessageBusService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public RabbitMqService(IConfiguration configuration)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();

            _channel.QueueDeclareAsync(
                queue: "motorcycles-registered", 
                durable: true, 
                exclusive: false,
                autoDelete: false,
                arguments: null
                ).GetAwaiter().GetResult();
        }

        public async Task PublishAsync(object data)
        {
            var type = data.GetType();

            var payload = JsonConvert.SerializeObject(data);
            var byteArray = Encoding.UTF8.GetBytes(payload);

            await _channel.BasicPublishAsync(
                exchange: string.Empty, 
                routingKey: "motorcycles-registered", 
                mandatory: true,
                basicProperties: new BasicProperties { Persistent = true },
                body: byteArray);
        }
    }
}
