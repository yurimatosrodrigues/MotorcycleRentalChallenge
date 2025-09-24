namespace MotorcycleRentalChallenge.Core.Entities
{
    public class Rental : BaseEntity
    {
        public Rental(Guid motorcycleId, Guid deliveryDriverId, RentalPlan rentalPlan, DateTime startDate) {
            MotorcycleId = motorcycleId;
            DeliveryDriverId = deliveryDriverId;
            RentalPlanId = rentalPlan.Id;            
            DailyRate = rentalPlan.DailyRate;
            
            StartDate = CreatedAt.AddDays(1);
            ExpectedEndDate = StartDate.AddDays(rentalPlan.Days);
        }

        public Guid MotorcycleId { get; private set; }
        public Motorcycle Motorcycle { get; private set; }
        public Guid DeliveryDriverId { get; private set; }
        public DeliveryDriver DeliveryDriver { get; private set; }
        public Guid RentalPlanId { get; private set; }
        public RentalPlan Plan { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime ExpectedEndDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public decimal DailyRate { get; private set; }
        public decimal TotalCost { get; private set; }
    }
}
