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
            ExpectedEndDate = CreatedAt.AddDays(rentalPlan.Days);

            Validate();
        }

        public Rental(Guid motorcycleId, Guid deliveryDriverId, Guid rentalplanId, RentalPlan rentalPlan,
            DateTime startDate, DateTime endDate, DateTime expectedEndDate)
        {
            MotorcycleId = motorcycleId;

            DeliveryDriverId = deliveryDriverId;

            RentalPlanId = rentalPlan.Id;
            RentalPlan = rentalPlan;

            DailyRate = rentalPlan.DailyRate;

            StartDate = startDate;
            EndDate = endDate;
            ExpectedEndDate = expectedEndDate;

            Validate();
            ValidateReturnDate(endDate);
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
        
        ///Return Date
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
            if (ExpectedEndDate <= StartDate)
            {
                throw new DomainException("Expected end date must be after start date.");
            }
        }

        private void ValidateReturnDate(DateTime returnDate)
        {
            if (returnDate < StartDate)
            {
                throw new DomainException("Return Date must be greater than Start Date.");
            }
        }

        public decimal CalculateTotalRentalCost(DateTime rentalEndDate)
        {
            ValidateReturnDate(rentalEndDate);

            decimal totalCost = 0;

            int daysRented = (rentalEndDate.Date - StartDate.Date).Days + 1;

            totalCost = daysRented * RentalPlan.DailyRate;

            if(rentalEndDate < ExpectedEndDate)
            {
                int daysLeft = RentalPlan.Days - daysRented;

                decimal penalty = daysLeft * RentalPlan.DailyRate * RentalPlan.PenaltyPercentageForUnusedDays ?? 0;
                                
                totalCost = totalCost + penalty;
            }
            else if(rentalEndDate > ExpectedEndDate)
            {
                int daysExceeded = (rentalEndDate.Date - ExpectedEndDate.Date).Days + 1;
                totalCost = RentalPlan.Days * RentalPlan.DailyRate;
                totalCost = totalCost + (daysExceeded * 50m);
            }

            return totalCost;
        }

        public decimal CompleteRent(DateTime rentalEndDate)
        {           
            TotalCost = CalculateTotalRentalCost(rentalEndDate);
            EndDate = rentalEndDate;

            return TotalCost;
        }
    }
}
