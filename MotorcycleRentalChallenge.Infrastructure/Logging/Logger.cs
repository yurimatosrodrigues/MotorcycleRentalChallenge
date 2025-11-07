using Serilog;

namespace MotorcycleRentalChallenge.Infrastructure.Logging
{
    public class Logger<T> : IAppLogger<T>
    {
        private readonly ILogger _logger;
        public Logger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogInformation(string message, params object[] args)
        {
            _logger.Information(message, args);
        }
    }
}
