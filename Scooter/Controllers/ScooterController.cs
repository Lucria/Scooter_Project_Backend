using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beam_intern.Scooter.Domain;
using Beam_intern.Scooter.Models;
using Beam_intern.Scooter.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beam_intern.Scooter.Controllers
{
    // REST controller
    [ApiController]
    // URL extension
    [Route("scooters/")]
    public class ScooterController : ControllerBase
    {
        private readonly IScooterRepository _repository;
        private readonly ILogger<ScooterController> _logger;

        public ScooterController(ILogger<ScooterController> logger, IScooterRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScooterDomainModel>>> GetAll()
        {
            var result = await _repository.GetAll();
            return Ok(result.Select(ToScooterResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScooterDomainModel>> Get(Guid id)
        {
            var result = await _repository.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(ToScooterResponse(result));
        }

        private ScooterResponseModel ToScooterResponse (ScooterDomainModel scooterDomainModel)
        {
            return new ScooterResponseModel(
                scooterDomainModel.Id, 
                scooterDomainModel.Latitude, 
                scooterDomainModel.Longitude);
        }
    }
}
