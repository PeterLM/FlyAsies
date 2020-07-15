using FlightSimulator.Services;
using FlightSimulator.Services.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FlightSimulatorTests
{
    [TestClass]
    public class AirportTests
    {
        [TestMethod]
        public void GetAirport()
        {
            var s = new AirportService(new TestStorageService());
            var a = s.GetByIataCode("BLL");
            Assert.AreEqual<string>("BLL", a.IataCode);
        }

        [TestMethod]
        public void AddAirport()
        {
            var s = new AirportService(new TestStorageService());
            var a = new Airport
            {
                IataCode = "ARN",
                Name = "Arlanda",
                Latitude = 50.1234,
                Longitude = 51.456,

            };
            s.Create(a);
            var na = s.GetByIataCode("ARN");
            Assert.AreEqual<string>("ARN", a.IataCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void AddExistingAirport()
        {
            var s = new AirportService(new TestStorageService());
            var a = new Airport
            {
                IataCode = "BLL",
                Name = "Billund",
                Latitude = 50.1234,
                Longitude = 51.456,

            };
            s.Create(a);
        }

        [TestMethod]
        public void DeleteAirport()
        {
            var s = new AirportService(new TestStorageService());
            s.Delete("BLL");
            Assert.AreEqual<int>(2, s.Get().Count);
        }
    }
}
