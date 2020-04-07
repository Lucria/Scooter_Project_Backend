using Beam_intern.Scooter.CoordinatesHelper;

namespace Beam_intern.Scooter.Domain
{
    public class ScooterClosestDomainModel
    {
        public ScooterClosestDomainModel(int x, int y, double latitude, double longitude)
        {
            CentreCoordinate = new Coordinates(latitude, longitude);
            NearestNumberOfScooters = x;
            Radius = y;
        }
        public int NearestNumberOfScooters { get; set; }
        public int Radius { get; set; }
        public Coordinates CentreCoordinate { get; set; }
    }
}