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
    public class SimulationController : ControllerBase
    {
        private readonly ISimulationService simulationService;
        private readonly ILogger<SimulationController> logger;

        public SimulationController(ISimulationService simulationService, ILogger<SimulationController> logger)
        {
            this.simulationService = simulationService;
            this.logger = logger;
        }

        [HttpGet("{fromiATA}/{toiATA}/{aircraftModel}", Name = "Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SimulationResult> Get(string fromiATA, string toiATA, string aircraftModel)
        {
            try
            {
                return simulationService.SimulateFlight(fromiATA, toiATA, aircraftModel);
            }
            catch (Exception ex)
            {
                logger.LogError(1, ex, "Get Aircrafts");
                return BadRequest(ex.Message);
            }
        }
    }
}
