using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services.Models
{
    public class SimulationResult
    {
        public TimeSpan Timespan { get; set; }
        public int FuelConsumption { get; set; }
        public int Distance { get; set; }
    }
}