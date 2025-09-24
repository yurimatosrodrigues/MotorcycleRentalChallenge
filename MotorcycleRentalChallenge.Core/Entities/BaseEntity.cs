namespace MotorcycleRentalChallenge.Core.Entities
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
    }
}
