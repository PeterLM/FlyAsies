using FlightSimulator.Services.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services
{
    public interface IAirportService
    {
        void Create(Airport airport);
        void Delete(Airport airport);
        void Delete(string iataCode);
        IList<Airport> Get();
        Airport GetByIataCode(string iataCode);
        void Update(Airport airport);
    }

    public class AirportService : IAirportService
    {
        private readonly IStorageService storageService;

        public AirportService(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public IList<Airport> Get()
        {
            return storageService.Airports;
        }

        public Airport GetByIataCode(string iataCode)
        {
            var r = storageService.Airports.SingleOrDefault(c => string.Compare(c.IataCode, iataCode, true) == 0);
            if (r == null)
            {
                throw new ArgumentException($"No airport with iATA code {iataCode}");
            }
            return r;
        }

        public void Create(Airport airport)
        {
            airport.IataCode = airport.IataCode.ToUpper();
            if (storageService.Airports.SingleOrDefault(c => c.IataCode == airport.IataCode) != null)
            {
                throw new ArgumentException($"Airport with iATA code {airport.IataCode} already exists.");
            }
            storageService.Airports.Add(airport);
        }

        public void Update(Airport airport)
        {
            Delete(airport);
            Create(airport);
        }
        public void Delete(Airport airport)
        {
            Delete(airport.IataCode);
        }

        public void Delete(string iataCode)
        {
            var storedAirport = GetByIataCode(iataCode);
            storageService.Airports.Remove(storedAirport);
        }
    }
}
