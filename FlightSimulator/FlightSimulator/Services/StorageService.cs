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
        List<Aircraft> Aircraft { get; }
        List<Airport> Airports { get; }
    }

    internal class StorageService : IStorageService
    {
        public StorageService()
        {
            this.Airports.Add(new Airport { IataCode = "BLL", Name = "Billund" });
            this.Airports.Add(new Airport { IataCode = "LHR", Name = "London Heathrow" });
            this.Airports.Add(new Airport { IataCode = "CDG", Name = "Charles de Gaulle" });
        }

        public List<Airport> Airports { get; private set; } = new List<Airport>();
        public List<Aircraft> Aircraft { get; private set; } = new List<Aircraft>();

    }
}
