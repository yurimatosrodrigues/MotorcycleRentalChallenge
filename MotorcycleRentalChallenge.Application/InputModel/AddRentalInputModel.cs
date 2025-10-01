using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Exceptions;
using System.Text.Json.Serialization;

namespace MotorcycleRentalChallenge.Application.InputModel
{
    public class AddRentalInputModel
    {
        [JsonPropertyName("entregador_id")]
        public string DeliveryDriverId { get; set; }

        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTime? EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime ExpectedEndDate { get; set; }

        [JsonPropertyName("plano")]
        public int RentalPlanDays { get; set; }

        public Rental ToEntity(RentalPlan rentalPlan)
        {
            if (!Guid.TryParse(DeliveryDriverId, out var guidDeliveryDriver))
            {
                throw new DomainException("Invalid Delivery Driver Id.");
            }

            if (!Guid.TryParse(MotorcycleId, out var guidMotorcycle))
            {
                throw new DomainException("Invalid Motorcycle Id.");
            }

            return new Rental(guidMotorcycle, guidDeliveryDriver, rentalPlan.Id, rentalPlan);
        }
    }
}
