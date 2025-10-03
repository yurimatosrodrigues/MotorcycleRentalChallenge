namespace MotorcycleRentalChallenge.Infrastructure.Messaging
{
    public interface IMessageBusService
    {
        Task PublishAsync(object data);
    }
}
