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
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly IAircraftService AircraftService;
        private readonly ILogger<AircraftService> logger;

        public AircraftController(IAircraftService AircraftService, ILogger<AircraftService> logger)
        {
            this.AircraftService = AircraftService;
            this.logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IList<Aircraft>> Get()
        {
            try
            {
                return Ok(AircraftService.Get());
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "Get Aircrafts");
                return BadRequest();
            }
        }

        [HttpGet("{model}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Aircraft> Get(string model)
        {
            try
            {
                return Ok(AircraftService.GetByModel(model));
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "No aircraft found with model {1}", model);
                return NotFound($"No aircraft found with model {model}");
            }
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Aircraft> Put([FromBody] Aircraft aircraft)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AircraftService.Update(aircraft);
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
                logger.LogError(1, ex, "No aircraft found with model {1}", aircraft.Model);
                return NotFound($"No aircraft found with model {aircraft.Model}");
            }
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Aircraft> Post([FromBody] Aircraft Aircraft)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AircraftService.Create(Aircraft);
                    return CreatedAtAction(nameof(Get), new { model = Aircraft.Model }, Aircraft);
                }
                else
                {
                    logger.LogError(1, "Invalid or missing parameters");
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "No aircraft found with model {1}", Aircraft.Model);
                return NotFound($"No aircraft found with model {Aircraft.Model}");
            }
        }

        [HttpDelete("{model}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(string model)
        {
            try
            {
                AircraftService.Delete(model);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "No Aircraft found with iATA code {1}", model);
                return NotFound($"No Aircraft found with iATA code {model}");
            }
        }
    }
}