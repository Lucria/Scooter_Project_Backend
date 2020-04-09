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
        // _logger is for future usages as logging is important in spotting bugs.
        private readonly ILogger<ScooterController> _logger;
        private readonly IScooterService _scooterService;

        public ScooterController(ILogger<ScooterController> logger, IScooterService scooterService)
        {
            _logger = logger;
            _scooterService = scooterService;
        }

        /**
         * GET API for all scooters present in the database
         * Returns either an error codee 404 if the operation failed
         * Or a response code 200 with the details of all scooters in the database.
         */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ScooterDomainModel>>> GetAll()
        {
            var result = await _scooterService.GetAll();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.Select(ToScooterResponse));
        }

        /**
         * GET API for a specific scooter.
         * One of the 4 main HTTP requests (GET, POST, PUT, DELETE)
         * Takes in a UUID and returns either
         * an error code 404 if the operation failed
         * or a response code 200 and the details of the specific scooter if it can be found.
         */
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

        /**
         * POST API for the main part of the project:
         * Getting X nearest scooters within Y metres at a chosen latitude and longitude
         * Takes in a new request model called ScooterClosestRequestModel
         * that requires NearestNumberOfScooters, Radius and a ChosenLatitude and ChosenLongitude.
         * Controller will call on the ScooterService to process the domain logic before returning
         * either an error code 404 if operation is not completed successfully
         * or an response code 200 and a List of ScooterResponseModel of the chosen Scooters. 
         */
        [HttpPost("closest/")]
        public ActionResult<IEnumerable<ScooterClosestDomainModel>> GetClosest([FromBody] ScooterClosestRequestModel scooterClosestRequestModel)
        {
            ScooterClosestDomainModel scooterClosestDomainModel = ToScooterClosestDomainModel(scooterClosestRequestModel);
            var result =  _scooterService.GetClosest(scooterClosestDomainModel);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result.Select(ToScooterResponse));
        }

        /**
         * POST API to create a new Scooter in the database with specific coordinates
         * One of the 4 main HTTP requests (GET, POST, PUT, DELETE)
         * Takes in a ScooterRequestModel, which requires new latitude and longitude.
         * Returns either an error code 404 if operation is not completed correctly.
         * Or returns response code 201 to indicate scooter object is created successfully.
         */
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

        /**
         * PUT API for a Scooter using a specific GUID and new Coordinates
         * One of the 4 main HTTP requests (GET, POST, PUT, DELETE)
         * Takes in a Guid id and a ScooterRequestModel. ScooterRequestModel requires new latitude and longitude.
         * Returns either an error code 404 if desired scooter is not present
         * Or returns response code 200 if desired scooter is present.
         */
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

        /**
         * Delete API for a Scooter using Guid.
         * One of the 4 main HTTP requests (GET, POST, PUT, DELETE)
         * Takes in a Guid id and returns either an error when the desired scooter doesn't exist
         * Or Response Code 204 to indicate that desired scooter has been deleted.
         */
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var result = await _scooterService.Delete(id);
            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        /**
         * Method responsible for converting a Domain model to a Response model for the View layer
         * As this project is a small scale one, ScooterResponseModel is the same as ScooterDomainModel.
         * In larger scale projects, the Response model may be very different compared to the Domain model.
         */
        private ScooterResponseModel ToScooterResponse (ScooterDomainModel scooterDomainModel)
        {
            return new ScooterResponseModel(
                scooterDomainModel.Id, 
                scooterDomainModel.Latitude, 
                scooterDomainModel.Longitude);
        }

        /**
         * Method responsible for converting a ClosestRequestModel to a ClosetDomainModel for
         * domain logic in the Services.
         * As this project is small scale, ClosestDomainModel is similar to ClosestRequestModel.
         */
        private ScooterClosestDomainModel ToScooterClosestDomainModel(
            ScooterClosestRequestModel scooterClosestRequestModel)
        {
            ScooterClosestDomainModel scooterClosestDomainModel = new ScooterClosestDomainModel(
                scooterClosestRequestModel.NearestNumberOfScooters,
                scooterClosestRequestModel.Radius,
                scooterClosestRequestModel.ChosenLatitude,
                scooterClosestRequestModel.ChosenLongitude);
            return scooterClosestDomainModel;
        }
    }
}
