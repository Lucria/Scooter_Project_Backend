using System;
using System.ComponentModel.DataAnnotations;

namespace Beam_intern.Scooter.Models
{
    public class ScooterDataModel
    // Data model for communication between DB and Backend
    {
        public ScooterDataModel(Guid id, double latitude, double longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        [Key]
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}