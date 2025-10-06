using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Enums;
using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.Test.Core
{
    [TestClass]
    public sealed class DeliveryDriverTest
    {   
        private readonly string _validIdentifier = "driver123";
        private readonly string _validName = "Yuri Matos";
        
        private readonly string _validCnpjMasked = "99.999.999/0001-99";
        private readonly string _validCnpjSanitized = "99999999000199";
        
        private readonly DateTime _validBirthdate = DateTime.UtcNow.AddYears(-20).Date;
        private readonly string _validCnhNumber = "1234567890123";
        private readonly CnhType _validCnhType = CnhType.AB;
        private readonly string _validCnhImagePath = "image.png";

        [TestMethod]
        public void DeliveryDriver_Created_Success()
        {
            var driver = new DeliveryDriver(
                _validIdentifier,
                _validName,
                _validCnpjMasked,
                _validBirthdate,
                _validCnhNumber,
                _validCnhType,
                _validCnhImagePath
            );
                        
            Assert.IsNotNull(driver);
            Assert.AreEqual(_validIdentifier, driver.Identifier);
            Assert.AreEqual(_validName, driver.Name);
            Assert.AreEqual(_validCnpjSanitized, driver.Cnpj);
            Assert.AreEqual(_validBirthdate, driver.Birthdate);
            Assert.AreEqual(_validCnhNumber, driver.CnhNumber);
            Assert.AreEqual(_validCnhType, driver.CnhType);
            Assert.AreEqual(_validCnhImagePath, driver.CnhImagePath);
            Assert.IsNotNull(driver.Rentals);
            Assert.AreEqual(0, driver.Rentals.Count);
            Assert.IsTrue(driver.Id.GetType() == typeof(Guid));
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Identifier is required.")]
        public void DeliveryDriver_InvalidIdentifier_DomainException()
        {
            var driver = new DeliveryDriver(
                string.Empty, _validName, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, _validCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Name is required.")]
        public void DeliveryDriver_InvalidName_DomainException()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, string.Empty, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, _validCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "CNPJ is required.")]
        public void DeliveryDriver_CnpjIsNullOrWhiteSpace_DomainException()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, string.Empty, _validBirthdate,
                _validCnhNumber, _validCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Invalid CNPJ.")]
        public void DeliveryDriver_CnpjLengthInvalid_DomainException()
        {            
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, "1234567890123", _validBirthdate,
                _validCnhNumber, _validCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Invalid birthdate.")]
        public void DeliveryDriver_BirthdateIsDefault_DomainException()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, default,
                _validCnhNumber, _validCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Invalid birthdate.")]
        public void DeliveryDriver_BirthdateTooOld_DomainException()
        {            
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, new DateTime(1899, 1, 1),
                _validCnhNumber, _validCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Delivery Driver must be over 18 years old.")]
        public void DeliveryDriver_Under18_DomainException()
        {            
            var birthdateUnder18 = DateTime.UtcNow.Date.AddYears(-18).AddDays(1);
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, birthdateUnder18,
                _validCnhNumber, _validCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Invalid CNH Number.")]
        public void DeliveryDriver_InvalidCnhNumber_DomainException()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, _validBirthdate,
                string.Empty, _validCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Invalid CNH Type.")]
        public void DeliveryDriver_InvalidCnhType_DomainException()
        {            
            var invalidCnhType = (CnhType)99;
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, invalidCnhType, _validCnhImagePath
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Invalid CNH Image.")]
        public void DeliveryDriver_CnhImagePathIsNullOrWhiteSpace_DomainException()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, _validCnhType, string.Empty
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "CNH image format must be PNG or BMP.")]
        public void DeliveryDriver_CnhImageFormatInvalid_DomainException()
        {            
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, _validCnhType, "image.jpg"
            );
        }

        [TestMethod]
        public void HasValidCnhToRentMotorcycle_CnhTypeA_ReturnsTrue()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, CnhType.A, _validCnhImagePath
            );

            Assert.IsTrue(driver.HasValidCnhToRentMotorcycle());
        }

        [TestMethod]
        public void HasValidCnhToRentMotorcycle_CnhTypeAB_ReturnsTrue()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, CnhType.AB, _validCnhImagePath
            );

            Assert.IsTrue(driver.HasValidCnhToRentMotorcycle());
        }

        [TestMethod]
        public void HasValidCnhToRentMotorcycle_CnhTypeB_ReturnsFalse()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, CnhType.B, _validCnhImagePath
            );

            Assert.IsFalse(driver.HasValidCnhToRentMotorcycle());
        }

        [TestMethod]
        public void UpdateCnhImage_ValidPathPng_Success()
        {
            var driver = new DeliveryDriver(
                _validIdentifier, _validName, _validCnpjSanitized, _validBirthdate,
                _validCnhNumber, _validCnhType, "old/path/cnh.bmp"
            );

            const string newPath = "new/path/image.png";
            driver.UpdateCnhImage(newPath);

            Assert.AreEqual(newPath, driver.CnhImagePath);
        }
    }
}
