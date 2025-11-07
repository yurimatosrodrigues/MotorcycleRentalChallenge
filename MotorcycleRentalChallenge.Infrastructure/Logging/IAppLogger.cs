using Serilog;

namespace MotorcycleRentalChallenge.Infrastructure.Logging
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message, params object[] args);
    }
}
