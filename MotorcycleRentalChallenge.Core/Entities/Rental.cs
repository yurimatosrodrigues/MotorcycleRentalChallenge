using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.Core.Entities
{
    public class Rental : BaseEntity
    {
        public Rental(Guid motorcycleId, Guid deliveryDriverId, Guid rentalplanId, RentalPlan rentalPlan) {
            MotorcycleId = motorcycleId;
            
            DeliveryDriverId = deliveryDriverId;
            
            RentalPlanId = rentalPlan.Id;
            RentalPlan = rentalPlan;

            DailyRate = rentalPlan.DailyRate;
            
            StartDate = CreatedAt.AddDays(1);
            ExpectedEndDate = StartDate.AddDays(rentalPlan.Days);

            Validate();
        }

        protected Rental() { }

        public Guid MotorcycleId { get; private set; }
        public virtual Motorcycle Motorcycle { get; private set; }
        public Guid DeliveryDriverId { get; private set; }
        public virtual DeliveryDriver DeliveryDriver { get; private set; }
        public Guid RentalPlanId { get; private set; }
        public virtual RentalPlan RentalPlan { get; private set; }

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
            ValidateDates();
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

        private void ValidateDates()
        {
            if (StartDate < DateTime.UtcNow.Date)
            {
                throw new DomainException("Start date must be in the future.");
            }

            if (ExpectedEndDate <= StartDate)
            {
                throw new DomainException("Expected end date must be after start date.");
            }
        }

        private void ValidateEndDate()
        {            
            if(EndDate < StartDate)
            {
                throw new DomainException("End date invalid.");
            }
        }

        public decimal CalculateTotalRentalCost(DateTime rentalEndDate)
        {
            EndDate = rentalEndDate;

            ValidateEndDate();

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

            TotalCost = totalCost;

            return totalCost;
        }
    }
}
