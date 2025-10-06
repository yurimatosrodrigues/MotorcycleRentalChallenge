namespace MotorcycleRentalChallenge.Core.Events
{
    public class MotorcycleRegisteredEvent
    {
        public MotorcycleRegisteredEvent(Guid id, string identifier, int year, string model, string plate) 
        {
            Id = id;
            Identifier = identifier;
            Year = year;
            Model = model;
            Plate = plate;
        }

        public Guid Id { get; private set; }
        public string Identifier { get; private set; }
        public int Year { get; private set; }
        public string Model { get; private set; }
        public string Plate { get; private set; }
    }
}
