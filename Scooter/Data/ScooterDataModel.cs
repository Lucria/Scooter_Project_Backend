using System;

namespace Beam_intern.Scooter.Data
{
    public class ScooterDataModel
    // Data model for communication between DB and Backend
    {
        public Guid Id { get; set; }
        // public GeoCoordinate coordinate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}