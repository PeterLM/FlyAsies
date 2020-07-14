using FlightSimulator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlightSimulatorTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddAirport()
        {
            var s = new AirportService(new StorageService());
            var a = s.GetByIatacode("BLL");
            Assert.AreEqual<string>("BLL", a.IataCode);
        }
    }
}
