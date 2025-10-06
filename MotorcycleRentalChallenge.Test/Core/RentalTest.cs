using MotorcycleRentalChallenge.Core.Entities;
using MotorcycleRentalChallenge.Core.Exceptions;

namespace MotorcycleRentalChallenge.Test.Core
{
    [TestClass]
    public sealed class RentalTest
    {
        private Guid _validMotorcycleId;
        private Guid _validDeliveryDriverId;
        private RentalPlan _sevenDaysPlan;

        [TestInitialize]
        public void TestInitialize()
        {
            _validMotorcycleId = Guid.NewGuid();
            _validDeliveryDriverId = Guid.NewGuid();

            _sevenDaysPlan = new RentalPlan(7, 30, 0.2m);
        }

        [TestMethod]
        public void Rental_WithOutDates_Success()
        {
            
            var rental = new Rental(
                _validMotorcycleId,
                _validDeliveryDriverId,
                _sevenDaysPlan.Id,
                _sevenDaysPlan
            );
            
            Assert.AreEqual(DateTime.UtcNow.Date.AddDays(1), rental.StartDate.Date);            
            Assert.AreEqual(rental.StartDate.Date.AddDays(6), rental.ExpectedEndDate.Date);
            Assert.AreEqual(_sevenDaysPlan.DailyRate, rental.DailyRate);
            Assert.IsNull(rental.EndDate);
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Motorcycle invalid.")]
        public void Rental_InvalidMotorcycleId_DomainException()
        {
            var rental = new Rental(
                Guid.Empty, _validDeliveryDriverId, _sevenDaysPlan.Id, _sevenDaysPlan
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Delivery driver invalid.")]
        public void Rental_InvalidDeliveryDriverId_DomainException()
        {
            var rental = new Rental(
                _validMotorcycleId, Guid.Empty, _sevenDaysPlan.Id, _sevenDaysPlan
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Expected end date must be after start date.")]
        public void Rental_ExpectedEndDateBeforeStartDate_DomainException()
        {
            var startDate = DateTime.UtcNow.AddDays(-5);
            
            var expectedEndDate = startDate.AddDays(-1);
            var endDate = expectedEndDate.AddDays(1);

            var rental = new Rental(
                _validMotorcycleId, _validDeliveryDriverId, _sevenDaysPlan.Id, _sevenDaysPlan,
                startDate, endDate, expectedEndDate
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Return Date must be greater than Start Date.")]
        public void Rental_EndDateBeforeStartDate_DomainException()
        {
            var startDate = DateTime.UtcNow.AddDays(-5);
            var expectedEndDate = startDate.AddDays(7);            
            var endDate = startDate.AddDays(-1);

            var rental = new Rental(
                _validMotorcycleId, _validDeliveryDriverId, _sevenDaysPlan.Id, _sevenDaysPlan,
                startDate, endDate, expectedEndDate
            );
        }

        [TestMethod]
        [ExpectedException(typeof(DomainException), "Return Date must be greater than Start Date.")]
        public void CalculateTotalRentalCost_EndDateBeforeStartDate_DomainException()
        {            
            var rental = new Rental(
                _validMotorcycleId, _validDeliveryDriverId, _sevenDaysPlan.Id, _sevenDaysPlan
            );         
            rental.CalculateTotalRentalCost(rental.StartDate.AddDays(-1));
        }

        [TestMethod]
        public void CalculateTotalRentalCost_ReturnOnTime_CorrectCost()
        {
            var rental = new Rental(
                _validMotorcycleId, _validDeliveryDriverId, _sevenDaysPlan.Id, _sevenDaysPlan
            );            
            var returnDate = rental.ExpectedEndDate;
                        
            var expectedCost = _sevenDaysPlan.Days * _sevenDaysPlan.DailyRate;
            var actualCost = rental.CalculateTotalRentalCost(returnDate);

            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void CalculateTotalRentalCost_EarlyReturn_CorrectCostWithPenalty()
        {
            var rental = new Rental(
                _validMotorcycleId, _validDeliveryDriverId, _sevenDaysPlan.Id, _sevenDaysPlan
            );
            
            var returnDate = rental.ExpectedEndDate.AddDays(-2);
            var daysRented = (returnDate.Date - rental.StartDate.Date).Days + 1;            

            var daysLeft = _sevenDaysPlan.Days - daysRented;            
            var expectedCost = (daysRented * _sevenDaysPlan.DailyRate) +
                               (daysLeft * _sevenDaysPlan.DailyRate * _sevenDaysPlan.PenaltyPercentageForUnusedDays.GetValueOrDefault());

            var actualCost = rental.CalculateTotalRentalCost(returnDate);

            Assert.AreEqual(162.00m, actualCost);
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void CalculateTotalRentalCost_LateReturn_CorrectCostWithExtraFee()
        {
            var rental = new Rental(
                _validMotorcycleId, _validDeliveryDriverId, _sevenDaysPlan.Id, _sevenDaysPlan
            );

            
            var returnDate = rental.ExpectedEndDate.AddDays(2);                        
            var baseCost = _sevenDaysPlan.Days * _sevenDaysPlan.DailyRate;

            var daysExceeded = (returnDate.Date - rental.ExpectedEndDate.Date).Days + 1;
                        
            var expectedCost = baseCost + (daysExceeded * 50.00m);

            var actualCost = rental.CalculateTotalRentalCost(returnDate);

            Assert.AreEqual(360.00m, actualCost);
            Assert.AreEqual(expectedCost, actualCost);
        }

        [TestMethod]
        public void CompleteRent_Success_SetsTotalCostAndEndDate()
        {
            var rental = new Rental(
                _validMotorcycleId, _validDeliveryDriverId, _sevenDaysPlan.Id, _sevenDaysPlan
            );
            
            var returnDate = rental.ExpectedEndDate;
            var expectedCost = _sevenDaysPlan.Days * _sevenDaysPlan.DailyRate;
            var finalCost = rental.CompleteRent(returnDate);

            Assert.AreEqual(expectedCost, finalCost);
            Assert.AreEqual(expectedCost, rental.TotalCost);
            Assert.AreEqual(returnDate, rental.EndDate);
        }
    }
}
