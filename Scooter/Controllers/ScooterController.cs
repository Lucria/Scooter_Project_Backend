using System;
using System.Collections.Generic;
using System.Linq;
using Beam_intern.Scooter.Data;
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
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ScooterController> _logger;

        public ScooterController(ILogger<ScooterController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ScooterDataModel> GetAll()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new ScooterDataModel
            {
                Id = new Guid(),
                Latitude = rng.Next(-20, 55),
                Longitude = rng.Next(Summaries.Length)
            })
            .ToArray();
        }

        [HttpGet("{id}")]
        public ScooterDataModel Get(Guid id)
        {
            var rng = new Random();
            var test = new ScooterDataModel {Id = id};
            return test;
        }
    }
}
