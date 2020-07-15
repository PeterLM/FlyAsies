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
    [Route("api/[controller]")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportService airportService;
        private readonly ILogger<AirportService> logger;

        public AirportController(IAirportService airportService, ILogger<AirportService> logger)
        {
            this.airportService = airportService;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Airport> Get(string iataCode)
        {
            try
            {
                return Ok(airportService.GetByIataCode(iataCode));
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "No airport found with iATA code {1}", iataCode);
                return NotFound($"No airport found with iATA code {iataCode}");
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Airport> Put([FromBody] Airport airport)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    airportService.Update(airport);
                    return Ok();
                }
                else
                {
                    logger.LogError(1, "Invalid or missing parameters");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "No airport found with iATA code {1}", airport.IataCode);
                return NotFound($"No airport found with iATA code {airport.IataCode}");
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Airport> Post([FromBody] Airport airport)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    airportService.Create(airport);
                    return CreatedAtAction(nameof(Get), new { iataCode = airport.IataCode }, airport);
                }
                else
                {
                    logger.LogError(1, "Invalid or missing parameters");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "Post airport");
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{iataCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(string iataCode)
        {
            try
            {
                airportService.Delete(iataCode);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "No airport found with iATA code {1}", iataCode);
                return NotFound($"No airport found with iATA code {iataCode}");
            }
        }
    }
}