using System;
using System.Collections.Generic;
using System.Linq;
using Beam_intern.Scooter.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Beam_intern.Scooter.Controllers
{
    [ApiController]
    [Route("api/beam")]
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
        public IEnumerable<ScooterDataModel> Get()
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
    }
}
