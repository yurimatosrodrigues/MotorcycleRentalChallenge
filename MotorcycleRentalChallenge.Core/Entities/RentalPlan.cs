namespace MotorcycleRentalChallenge.Core.Entities
{
    public class RentalPlan : BaseEntity
    {
        public RentalPlan(int days, decimal dailyCost, decimal? penaltyPercentage) {
            Days = days;
            DailyRate = dailyCost;
            PenaltyPercentageForUnusedDays = penaltyPercentage;
        }

        public int Days { get; private set; }
        public decimal DailyRate { get; private set; }
        public decimal? PenaltyPercentageForUnusedDays { get; private set; }
    }
}
