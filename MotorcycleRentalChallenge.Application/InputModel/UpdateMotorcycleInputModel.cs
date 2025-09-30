using System.Text.Json.Serialization;

namespace MotorcycleRentalChallenge.Application.InputModel
{
    public class UpdateMotorcycleInputModel
    {
        [JsonPropertyName("placa")]
        public string Plate { get; set; }
    }
}
