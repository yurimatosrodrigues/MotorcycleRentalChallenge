namespace MotorcycleRentalChallenge.Core.Entities
{
    public class RentalPlan : BaseEntity
    {
        public RentalPlan(int days, decimal dailyRate, decimal? penaltyPercentageForUnusedDays) {
            Days = days;
            DailyRate = dailyRate;
            PenaltyPercentageForUnusedDays = penaltyPercentageForUnusedDays;

            Rentals = new List<Rental>();
        }

        public int Days { get; private set; }
        public decimal DailyRate { get; private set; }
        public decimal? PenaltyPercentageForUnusedDays { get; private set; }

        public virtual ICollection<Rental> Rentals { get; private set; }
    }
}
