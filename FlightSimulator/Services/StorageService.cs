using FlightSimulator.Services.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services
{
    public interface IStorageService
    {
        List<Aircraft> Aircrafts { get; }
        List<Airport> Airports { get; }
    }

    internal class StorageService : IStorageService
    {
        public StorageService()
        {
            this.Airports.Add(new Airport { IataCode = "BLL", Name = "Billund", Latitude = 55.743541, Longitude = 9.147464 });
            this.Airports.Add(new Airport { IataCode = "LHR", Name = "London Heathrow", Latitude = 51.476885, Longitude = -0.461136 });
            this.Airports.Add(new Airport { IataCode = "CDG", Name = "Charles de Gaulle", Latitude = 49.015605, Longitude = 2.536772 });

            this.Aircrafts.Add(new Aircraft { Fuel = 500, FuelConsumption = 50, Model = "Sesna", Speed = 350, Type = AircraftTypes.Privatfly });
            this.Aircrafts.Add(new Aircraft { Fuel = 3000, FuelConsumption = 200, Model = "Mig", Speed = 2000, Type = AircraftTypes.Jagerfly });
            this.Aircrafts.Add(new Aircraft { Fuel = 25000, FuelConsumption = 400, Model = "Concorde", Speed = 1500, Type = AircraftTypes.Passagerfly });
            this.Aircrafts.Add(new Aircraft { Fuel = 0, FuelConsumption = 0, Model = "Svæv", Speed = 150, Type = AircraftTypes.Svævefly });
        }

        public List<Airport> Airports { get; private set; } = new List<Airport>();
        public List<Aircraft> Aircrafts { get; private set; } = new List<Aircraft>();
    }
}
