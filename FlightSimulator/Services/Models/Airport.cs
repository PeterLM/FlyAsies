using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FlightSimulator.Services.Models
{
    public class Airport
    {
        [Required(ErrorMessage = "Provide an airport iATA code")]
        public string IataCode { get; set; }
        [Required(ErrorMessage = "Provide an airport name")]
        public string Name { get; set; }
        [Range(-90.0, 90.0, ErrorMessage = "Value must be between -90 and 90")]
        public double Latitude { get; set; }
        [Range(-180.0, 180.0, ErrorMessage = "Value must be between -180 and 180")]
        public double Longitude { get; set; }
    }
}
