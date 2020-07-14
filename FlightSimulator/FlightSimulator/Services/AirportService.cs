using FlightSimulator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services
{
    public interface IAirportService
    {
        void Create(Airport airport);
        void Delete(Airport airport);
        IList<Airport> Get();
        Airport GetByIatacode(string iataCode);
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

        public Airport GetByIatacode(string iataCode)
        {
            return storageService.Airports.Single(c => string.Compare(c.IataCode, iataCode, true) == 0);
        }

        public void Create(Airport airport)
        {
            storageService.Airports.Add(airport);
        }

        public void Update(Airport airport)
        {
            Delete(airport);
            storageService.Airports.Add(airport);
        }
        public void Delete(Airport airport)
        {
            var storedAirport = GetByIatacode(airport.IataCode);
            storageService.Airports.Remove(storedAirport);
        }
    }
}
