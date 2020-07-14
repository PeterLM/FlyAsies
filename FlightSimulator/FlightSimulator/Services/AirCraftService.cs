using FlightSimulator.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services
{
    public interface IAirCraftService
    {
        List<Aircraft> Get();
    }

    public class AirCraftService : IAirCraftService
    {
        private readonly IStorageService storageService;

        public AirCraftService(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        public List<Aircraft> Get()
        {
            return storageService.Aircraft;
        }
    }
}
