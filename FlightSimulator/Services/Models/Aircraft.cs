using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services.Models
{
    public enum AircraftTypes
    {
        Ukendt = 0,
        Svævefly = 1,
        Privatfly = 3,
        Passagerfly = 4,
        Transportfly = 5,
        Jagerfly = 6,
    }

    public class Aircraft
    {
        [Required(ErrorMessage="Select at type")]
        public AircraftTypes Type { get; set; }
        [Required(ErrorMessage = "Type a model")]
        public string Model { get; set; }
        [Min(0)]
        public double Fuel { get; set; }
        [Min(0)]
        public int Speed { get; set; }
        [Min(0)]
        public double FuelConsumption { get; set; }
    }
}