using System.Text.Json.Serialization;

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

        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("identificador")]
        public string Identifier { get; set; }

        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [JsonPropertyName("modelo")]
        public string Model { get; set; }
        
        [JsonPropertyName("placa")]
        public string Plate { get; set; }
    }
}
