using System.Text.Json.Serialization;

namespace MotorcycleRentalChallenge.Application.InputModel
{
    public class CnhImageInputModel
    {
        [JsonPropertyName("imagem_cnh")]
        public string CnhImageBase64 { get; set; }
    }
}
