namespace MotorcycleRentalChallenge.Core.Entities
{
    public class Motorcycle : BaseEntity
    {
        public Motorcycle(int year, string model, string plate)
        {
            Year = year;
            Model = model;
            Plate = plate.ToUpperInvariant();

            Rentals = new List<Rental>();
        }

        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Plate { get; private set; }
        
        public ICollection<Rental> Rentals { get; private set; }

    }
}
