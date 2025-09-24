using MotorcycleRentalChallenge.Core.Enums;

namespace MotorcycleRentalChallenge.Core.Entities
{
    public class DeliveryDriver : BaseEntity
    {
        public DeliveryDriver(string name, string cnpj, DateTime birthDate, 
            string cnhNumber, CnhType cnhType, string cnhImagePath)
        {
            Name = name;
            Cnpj = cnpj;
            BirthDate = birthDate;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CnhImagePath = cnhImagePath;

            Rentals = new List<Rental>();
        }

        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string CnhNumber { get; private set; }
        public CnhType CnhType { get; private set; }
        public string? CnhImagePath { get; private set; }

        public ICollection<Rental> Rentals { get; set; }
    }
}
