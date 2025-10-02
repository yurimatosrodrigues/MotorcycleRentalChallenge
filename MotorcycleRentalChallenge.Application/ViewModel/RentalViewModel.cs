using System.Text.Json.Serialization;

namespace MotorcycleRentalChallenge.Application.ViewModel
{
    public class RentalViewModel
    {
        public RentalViewModel(Guid id, decimal dailyRate, Guid deliveryDriverId, Guid motorcycleId,
            DateTime startDate, DateTime expectedEndDate, DateTime? returnDate)
        {
            Id = id;
            DailyRate = dailyRate;
            DeliveryDriverId = deliveryDriverId;
            MotorcycleId = motorcycleId;
            StartDate = startDate;
            EndDate = expectedEndDate; //ExpectedEndDate is always equal to end rental date.
            ExpectedEndDate = expectedEndDate;
            ReturnDate = returnDate;
        }

        [JsonPropertyName("identificador")]
        public Guid Id { get; set; }

        [JsonPropertyName("valor_diaria")]
        public decimal DailyRate { get; set; }

        [JsonPropertyName("entregador_id")]
        public Guid DeliveryDriverId { get; set; }

        [JsonPropertyName("moto_id")]
        public Guid MotorcycleId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime ExpectedEndDate { get; set; }

        [JsonPropertyName("data_devolucao")]
        public DateTime? ReturnDate { get; set; }
    }
}
