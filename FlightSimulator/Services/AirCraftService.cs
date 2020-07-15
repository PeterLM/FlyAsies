using FlightSimulator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services
{
    public interface IAircraftService
    {
        void Create(Aircraft aircraft);
        void Delete(Aircraft aircraft);
        void Delete(string model);
        List<Aircraft> Get();
        Aircraft GetByModel(string model);
        void Update(Aircraft aircraft);
    }

    public class AircraftService : IAircraftService
    {
        private readonly IStorageService storageService;

        public AircraftService(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public List<Aircraft> Get()
        {
            return storageService.Aircrafts;
        }

        public Aircraft GetByModel(string model)
        {
            var r= storageService.Aircrafts.SingleOrDefault(c => string.Compare(c.Model, model, true) == 0);
            if (r==null)
            {
                throw new ArgumentException($"No aircraft model {model}");
            }
            return r;
        }

        public void Create(Aircraft aircraft)
        {
            if (storageService.Aircrafts.SingleOrDefault(c => string.Compare(c.Model, aircraft.Model, true) == 0) != null)
            {
                throw new ArgumentException("That aircraft model {aircraft.Model} already exists.");
            }
            storageService.Aircrafts.Add(aircraft);
        }

        public void Update(Aircraft aircraft)
        {
            Delete(aircraft);
            Create(aircraft);
        }

        public void Delete(Aircraft aircraft)
        {
            Delete(aircraft.Model);
        }

        public void Delete(string model)
        {
            var storeAircraft = GetByModel(model);
            storageService.Aircrafts.Remove(storeAircraft);
        }
    }
}