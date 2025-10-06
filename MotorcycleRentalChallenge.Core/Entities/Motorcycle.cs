using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.Core.Entities
{
    public class Motorcycle : BaseEntity
    {
        public Motorcycle(string identifier, int year, string model, string plate)
        {
            Identifier = identifier;
            Year = year;
            Model = model;
            Plate = plate.ToUpperInvariant();

            Validate();

            Rentals = new List<Rental>();
        }

        public string Identifier { get; set; }

        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Plate { get; private set; }
        
        public virtual ICollection<Rental> Rentals { get; private set; }

        private void Validate()
        {
            ValidateIdentifier();
            ValidateYear();
            ValidateModel();
            ValidatePlate();
        }

        private void ValidateIdentifier()
        {
            if (string.IsNullOrWhiteSpace(Identifier))
            {
                throw new DomainException("Identifier is required.");
            }
        }

        private void ValidateYear()
        {
            if(Year < 1900 || Year > DateTime.UtcNow.Year + 1)
            {
                throw new DomainException($"Motorcycle year must be between 1900 and {DateTime.UtcNow.Year + 1}.");
            }
        }

        private void ValidateModel()
        {
            if (string.IsNullOrWhiteSpace(Model))
            {
                throw new DomainException("Motorcycle model is required.");
            }
        }

        private void ValidatePlate()
        {
            if (string.IsNullOrWhiteSpace(Plate)) 
            {
                throw new DomainException("Motorcycle plate is required.");
            }

            if(Plate.Length != 7 && Plate.Length != 8)
            {
                throw new DomainException("Motorcycle plate is invalid.");
            }
        }

        public void UpdatePlate(string newPlate)
        {
            Plate = newPlate.ToUpperInvariant();
            ValidatePlate();
        }

        public bool CanBeDeleted()
        {
            if (Rentals.Count == 0) return true;
            return false;
        }

        public bool CanBeRented(DateTime startRentalDate)
        {
            var lastRental = Rentals.OrderByDescending(x => x.CreatedAt).FirstOrDefault();
            if (lastRental != null) 
            { 
                if(!lastRental.EndDate.HasValue)
                {
                    return false;
                }
                if (lastRental.EndDate >= startRentalDate)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
