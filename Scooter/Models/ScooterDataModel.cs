using System;
using System.ComponentModel.DataAnnotations;

namespace Beam_intern.Scooter.Models
{
    public class ScooterDataModel
    // Data model for communication between DB and Backend
    {
        [Key]
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}