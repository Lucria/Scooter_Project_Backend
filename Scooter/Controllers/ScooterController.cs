using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beam_intern.Scooter.Domain;
using Beam_intern.Scooter.Models;
using Beam_intern.Scooter.Repository;
using Beam_intern.Scooter.Services;
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
        private readonly ILogger<ScooterController> _logger;
        private readonly IScooterService _scooterService;

        public ScooterController(ILogger<ScooterController> logger, IScooterService scooterService)
        {
            _logger = logger;
            _scooterService = scooterService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScooterDomainModel>>> GetAll()
        {
            var result = await _scooterService.GetAll();
            return Ok(result.Select(ToScooterResponse));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ScooterDomainModel>> Get(Guid id)
        {
            var result = await _scooterService.Get(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(ToScooterResponse(result));
        }

        [HttpPost("closest/")]
        public ActionResult<IEnumerable<ScooterClosestRequestModel>> GetClosest([FromBody] ScooterClosestRequestModel scooterClosestRequestModel)
        // X number of scooters
        // within Y metres radius
        // At chosen latitude and longitude
        {
            // Need change request model to a domain model
            ScooterClosestDomainModel scooterClosestDomainModel = new ScooterClosestDomainModel(
                scooterClosestRequestModel.NearestNumberOfScooters,
                scooterClosestRequestModel.Radius,
                scooterClosestRequestModel.ChosenLatitude,
                scooterClosestRequestModel.ChosenLongitude);
            var result =  _scooterService.GetClosest(scooterClosestDomainModel);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Select(ToScooterResponse));
        }

        [HttpPost]
        public async Task<ActionResult<ScooterDomainModel>> Post([FromBody] ScooterRequestModel requestModel)
        {
            ScooterDomainModel scooterDomainModel = new ScooterDomainModel(Guid.NewGuid(), requestModel.latitude, requestModel.longitude);
            var result = await _scooterService.Add(scooterDomainModel);
            if (result == null)
            {
                return NotFound();
            }

            return Created(Request.Path.Value + "/" + result.Id, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ScooterDomainModel>> Put(Guid id, [FromBody] ScooterRequestModel requestModel)
        {
            var result = await _scooterService.Update(id, requestModel.latitude, requestModel.longitude);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(ToScooterResponse(result));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ScooterDomainModel>> Delete(Guid id)
        {
            var result = await _scooterService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
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
