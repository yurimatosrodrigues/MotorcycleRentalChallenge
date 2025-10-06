using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.Test.Core
{
    [TestClass]
    public sealed class MotorcycleTest
    {
        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void Motorcycle_InvalidIdentifier_DomainException()
        {
            var motorcycle = new Motorcycle(string.Empty, 2025, "Model", "plate");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void Motorcycle_InvalidOlderYear_DomainException()
        {
            var motorcycle = new Motorcycle("Identifier123", 1800, "Model", "plate");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void Motorcycle_InvalidFutureYear_DomainException()
        {
            var motorcycle = new Motorcycle("Identifier123", 2200, "Model", "plate");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void Motorcycle_InvalidModel_DomainException()
        {
            var motorcycle = new Motorcycle("Identifier123", 2025, string.Empty, "plate");
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void Motorcycle_InvalidPlate_DomainException()
        {
            var motorcycle = new Motorcycle("Identifier123", 2025, "Model", string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void Motorcycle_InvalidLengthPlate_DomainException()
        {
            var motorcycle = new Motorcycle("Identifier123", 2025, "Model", "ABCDEFG123456");
        }

        [TestMethod]        
        public void Motorcycle_Created_Success()
        {
            var motorcycle = new Motorcycle("Identifier123", 2026, "Kawasaki Ninja", "ABC1234");

            Assert.IsNotNull(motorcycle.Id);
            Assert.IsTrue(motorcycle.Id.GetType() == typeof(Guid));
            Assert.AreEqual(motorcycle.Plate, "ABC1234");
            Assert.AreEqual(0, motorcycle.Rentals.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException))]
        public void UpdatePlate_InvalidLengthNewPlate_DomainException()
        {
            var motorcycle = new Motorcycle("Identifier123", 2025, "Model", "ABC1234");            
            motorcycle.UpdatePlate("INV123");
        }

        [TestMethod]
        public void CanBeDeleted_NoRentals_ReturnsTrue()
        {
            var motorcycle = new Motorcycle("Identifier123", 2025, "Model", "ABC1234");                        
            Assert.IsTrue(motorcycle.CanBeDeleted());
        }

        [TestMethod]
        public void CanBeDeleted_HasRentals_ReturnsFalse()
        {   
            var motorcycle = new Motorcycle("Identifier123", 2025, "Model", "ABC1234");

            var rentalsList = (ICollection<Rental>)motorcycle.Rentals;
            rentalsList.Add(new Rental(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new RentalPlan(7, 30, 0.2m)));

            Assert.IsFalse(motorcycle.CanBeDeleted());
        }

        [TestMethod]
        public void CanBeRented_NoRentals_ReturnsTrue()
        {
            var motorcycle = new Motorcycle("Identifier123", 2025, "Model", "ABC1234");

            Assert.IsTrue(motorcycle.CanBeRented());
        }

        [TestMethod]
        public void CanBeRented_LastRentalEndDateInFuture_ReturnsFalse()
        {
            var motorcycle = new Motorcycle("Identifier123", 2025, "Model", "ABC1234");
            var rentalsList = (ICollection<Rental>)motorcycle.Rentals;
            
            rentalsList.Add(new Rental(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new RentalPlan(7, 30, 0.2m)));

            Assert.IsFalse(motorcycle.CanBeRented());
        }
    }
}
