using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightSimulator.Services;
using FlightSimulator.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightSimulator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService airportService;
        private readonly ILogger<AirportService> logger;

        public AirportController(IAirportService airportService, ILogger<AirportService> logger)
        {
            this.airportService = airportService;
            this.logger = logger;
        }

        // GET: Airport
        [HttpGet]
        public ActionResult<IList<Airport>> Get()
        {
            try
            {
                return Ok(airportService.Get());
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "Get airports");
                return BadRequest();
            }
        }

        [HttpGet("{iataCode}")]
        public ActionResult<Airport> Get(string iataCode)
        {
            try
            {
                return Ok(airportService.GetByIatacode(iataCode));
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "Get airport with iATA code {1}", iataCode);
                return BadRequest($"Get airport with iATA code {iataCode}");
            }
        }
    }
}