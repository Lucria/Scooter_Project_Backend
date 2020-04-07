using System;

namespace Beam_intern.Scooter.Models
{
    public class ScooterResponseModel
    {
        public ScooterResponseModel(Guid id, double latitude, double longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }
        
        public Guid Id { get;}
        public double Latitude { get; }
        public double Longitude { get;}
    }
}