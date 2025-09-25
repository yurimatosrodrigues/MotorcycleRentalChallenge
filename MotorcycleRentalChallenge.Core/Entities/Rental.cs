using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.Core.Entities
{
    public class Rental : BaseEntity
    {
        public Rental(Guid motorcycleId, Guid deliveryDriverId, RentalPlan rentalPlan) {
            MotorcycleId = motorcycleId;
            
            DeliveryDriverId = deliveryDriverId;
            
            RentalPlanId = rentalPlan.Id;
            RentalPlan = rentalPlan;

            DailyRate = rentalPlan.DailyRate;
            
            StartDate = CreatedAt.AddDays(1);
            ExpectedEndDate = StartDate.AddDays(rentalPlan.Days);

            Validate();
        }

        public Guid MotorcycleId { get; private set; }
        public Motorcycle Motorcycle { get; private set; }
        public Guid DeliveryDriverId { get; private set; }
        public DeliveryDriver DeliveryDriver { get; private set; }
        public Guid RentalPlanId { get; private set; }
        public RentalPlan RentalPlan { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime ExpectedEndDate { get; private set; }
        public DateTime? EndDate { get; private set; }

        public decimal DailyRate { get; private set; }
        public decimal TotalCost { get; private set; }

        private void Validate()
        {
            ValidateMotorcycleId();
            ValidateDeliveryDriverId();
            ValidateRentalPlanId();
        }

        private void ValidateMotorcycleId()
        {
            if (MotorcycleId == Guid.Empty)
            {
                throw new DomainException("Motorcycle invalid.");
            }   
        }

        private void ValidateDeliveryDriverId()
        {
            if (DeliveryDriverId == Guid.Empty)
            {
                throw new DomainException("Delivery driver invalid.");
            }
        }

        private void ValidateRentalPlanId()
        {
            if (RentalPlanId == Guid.Empty)
            {
                throw new DomainException("Rental Plan invalid.");
            }
        }

        public decimal CalculateTotalRentalCost(DateTime rentalEndDate)
        {
            EndDate = rentalEndDate;

            decimal totalCost = 0;

            int daysRented = (rentalEndDate - StartDate).Days;
            totalCost = daysRented * RentalPlan.DailyRate;

            if(EndDate < ExpectedEndDate)
            {
                int daysLeft = RentalPlan.Days - daysRented;

                decimal penalty = daysLeft * RentalPlan.DailyRate * RentalPlan.PenaltyPercentageForUnusedDays ?? 0;
                                
                totalCost = totalCost + penalty;
            }
            else if(EndDate > ExpectedEndDate)
            {
                int daysExceeded = (rentalEndDate - ExpectedEndDate).Days;
                totalCost = RentalPlan.Days * RentalPlan.DailyRate;
                totalCost = totalCost + (daysExceeded * 50m);
            }

            return totalCost;
        }
    }
}
