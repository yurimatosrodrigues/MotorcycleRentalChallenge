using System.Text.Json.Serialization;

namespace MotorcycleRentalChallenge.Application.ViewModel
{
    public class DeliveryDriverViewModel
    {
        public DeliveryDriverViewModel(Guid id, string identifier, string name, string cnpj, DateTime birthdate,
            string cnhNumber, string cnhType)
        {
            Id = id;
            Identifier = identifier;
            Name = name;
            Cnpj = cnpj;
            Birthdate = birthdate;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
        }

        [JsonPropertyName("id")]
        public Guid Id { get; set; }

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
    }
}
