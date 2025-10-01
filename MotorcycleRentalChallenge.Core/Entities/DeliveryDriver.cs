using MotorcycleRentalChallenge.Core.Enums;
using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.Core.Entities
{
    public class DeliveryDriver : BaseEntity
    {
        public DeliveryDriver(string identifier, string name, string cnpj, DateTime birthdate, 
            string cnhNumber, CnhType cnhType, string cnhImagePath)
        {
            Identifier = identifier;
            Name = name;
            Cnpj = SanitizeCnpj(cnpj);
            Birthdate = birthdate;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CnhImagePath = cnhImagePath;

            Validate();

            Rentals = new List<Rental>();
        }

        public string Identifier { get; private set; }
        public string Name { get; private set; }
        public string Cnpj { get; private set; }
        public DateTime Birthdate { get; private set; }
        public string CnhNumber { get; private set; }
        public CnhType CnhType { get; private set; }
        public string CnhImagePath { get; private set; }
        
        public virtual ICollection<Rental> Rentals { get; private set; }

        private void Validate()
        {
            ValidateIdentifier();
            ValidateName();
            ValidateCnpj();
            ValidateBirthdate();
            ValidateCnhNumber();
            ValidateCnhType();
            ValidateCnhImagePath();
        }

        private void ValidateIdentifier()
        {
            if (string.IsNullOrWhiteSpace(Identifier))
            {
                throw new DomainException("Identifier is required.");
            }
        }

        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("Name is required.");
            }
        }

        private void ValidateCnpj()
        {
            if (string.IsNullOrWhiteSpace(Cnpj))
            {
                throw new DomainException("CNPJ is required.");
            }
            
            if(Cnpj.Length != 14)
            {
                throw new DomainException("Invalid CNPJ.");
            }
        }

        private void ValidateBirthdate()
        {
            if(Birthdate == default || Birthdate.Year < 1900)
            {
                throw new DomainException("Invalid birthdate.");
            }

            int age = DateTime.UtcNow.Year - Birthdate.Year;
            if (Birthdate.Date > DateTime.UtcNow.AddYears(-age))
            {
                age--;
            }

            if (age < 18)
            {
                throw new DomainException("Delivery Driver must be over 18 years old.");
            }
        }

        private void ValidateCnhNumber()
        {
            if (string.IsNullOrWhiteSpace(CnhNumber))
            {
                throw new DomainException("Invalid CNH Number.");
            }
        }

        private void ValidateCnhType()
        {
            if (!Enum.IsDefined(typeof(CnhType), CnhType)) 
            {
                throw new DomainException("Invalid CNH Type.");
            }                
        }

        private void ValidateCnhImagePath()
        {
            if (string.IsNullOrWhiteSpace(CnhImagePath))
            {
                throw new DomainException("Invalid CNH Image.");
            }
            string extension = Path.GetExtension(CnhImagePath).ToLowerInvariant();
            if(extension != ".png" && extension != ".bmp")
            {
                throw new DomainException("CNH image format must be PNG or BMP.");
            }
        }

        private string SanitizeCnpj(string cnpj)
        {
            return cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
        }

        public bool HasValidCnhToRentMotorcycle()
        {
            if(this.CnhType == CnhType.A)
            {
                return true;
            }
            return false;
        }

        public void UpdateCnhImage(string imagePath)
        {
            CnhImagePath = imagePath;
            ValidateCnhImagePath();
        }
    }
}
