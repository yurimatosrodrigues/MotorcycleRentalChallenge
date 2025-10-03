namespace MotorcycleRentalChallenge.Core.Entities
{
    public class MotorcycleNotification : BaseEntity
    {
        public MotorcycleNotification(Guid motorcycleId, int year, string plate)
        {
            MotorcycleId = motorcycleId;
            Year = year;
            Plate = plate;
        }
        public Guid MotorcycleId { get; private set; }        
        public int Year { get; private set; }        
        public string Plate { get; private set; }
    }
}
