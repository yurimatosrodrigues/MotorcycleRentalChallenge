using MotorcycleRentalChallenge.Application.Extensions;
using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Enums;
using System.Text.Json.Serialization;

namespace MotorcycleRentalChallenge.Application.InputModel
{
    public class AddDeliveryDriverInputModel
    {
        [JsonPropertyName("identificador")]
        public string Identifier { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; set; }

        [JsonPropertyName("data_nascimento")]
        public DateTime Birthdate { get; set; }

        [JsonPropertyName("numero_cnh")]
        public string CnhNumber { get; set; }

        [JsonPropertyName("tipo_cnh")]
        public string CnhType { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string CnhImage { get; set; }

        public DeliveryDriver ToEntity()
        {
            return new DeliveryDriver(
                Identifier, 
                Name, 
                Cnpj, 
                Birthdate.ToUniversalTime(), 
                CnhNumber,
                EnumParserExtension.ParseEnum<CnhType>(CnhType),
                CnhImage
                );
        }
    }
}
