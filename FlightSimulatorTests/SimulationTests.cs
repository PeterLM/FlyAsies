using FlightSimulator.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightSimulatorTests
{
    [TestClass]
    public class SimulationTests
    {
        [TestMethod]
        public void Test1Simulator()
        {
            var store = new TestStorageService();
            var ap1 = store.Airports.Single(c => c.IataCode == "BLL");
            var ap2 = store.Airports.Single(c => c.IataCode == "LHR");
            var ac = store.Aircrafts.Single(c => c.Model == "Concorde");

            var s = new SimulationService(new AirportService(store), new AircraftService(store));
            var r = s.SimulateFlight(ap1, ap2, ac);

            Assert.AreEqual<int>(790, r.Distance);
            Assert.AreEqual<int>(3162, r.FuelConsumption);
            Assert.AreEqual<TimeSpan>(r.Timespan, new TimeSpan(0, 31, 36));
        }

        [TestMethod]
        public void Test2Simulator()
        {
            var store = new TestStorageService();
            var s = new SimulationService(new AirportService(store), new AircraftService(store));
            var r = s.SimulateFlight("BLL", "LHR", "Concorde");

            Assert.AreEqual<int>(790, r.Distance);
            Assert.AreEqual<int>(3162, r.FuelConsumption);
            Assert.AreEqual<TimeSpan>(r.Timespan, new TimeSpan(0, 31, 36));
        }
    }
}
