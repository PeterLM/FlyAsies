using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services.Models
{
    public class Aircraft
    {
        public string Type { get; set; }
        public double Fuel { get; set; }
        public int Speed { get; set; }
        public double FuelConsumption { get; set; }
    }
}
