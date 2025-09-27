using System.Text.Json.Serialization;

namespace MotorcycleRentalChallenge.Application.InputModel
{
    public class UpdateMotorcycleInputModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("placa")]
        public string Plate { get; set; }
    }
}
