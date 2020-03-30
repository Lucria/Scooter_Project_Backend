using System;

namespace Beam_intern.Scooter.Domain
{
    public class Scooter
    {
        public Scooter(Guid guid, double latitude, double longitude)
        {
            Id = guid;
            Latitude = latitude;
            Longitude = longitude;
        }
        public Guid Id { get; }
        public double Latitude { get; }
        public double Longitude { get; }
        
    }
}