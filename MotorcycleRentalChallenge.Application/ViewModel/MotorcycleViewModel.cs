namespace MotorcycleRentalChallenge.Application.ViewModel
{
    public class MotorcycleViewModel
    {
        public MotorcycleViewModel(Guid id, string identifier, int year, string model, string plate)
        {
            Id = id;
            Identifier = identifier;
            Year = year;
            Model = model;
            Plate = plate;
        }

        public Guid Id { get; set; }
        public string Identifier { get; set; }
        public int Year { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}
