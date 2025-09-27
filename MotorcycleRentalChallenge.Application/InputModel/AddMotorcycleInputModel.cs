using MotorcycleRentalChallenge.Core.Entities;
using System.Text.Json.Serialization;

namespace MotorcycleRentalChallenge.Application.InputModel
{
    public class AddMotorcycleInputModel
    {
        [JsonPropertyName("identificador")]
        public string Identifier { get; set; }

        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [JsonPropertyName("modelo")]
        public string Model { get; set; }

        [JsonPropertyName("placa")]
        public string Plate { get; set; }

        public Motorcycle ToEntity() => new Motorcycle(Identifier, Year, Model, Plate);
    }
}
