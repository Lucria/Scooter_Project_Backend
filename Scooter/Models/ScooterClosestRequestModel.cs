namespace Beam_intern.Scooter.Models
{
    public class ScooterClosestRequestModel
    {
        public ScooterClosestRequestModel(int x, int y, double latitude, double longitude)
        {
            NearestNumberOfScooters = x;
            Radius = y;
            ChosenLatitude = latitude;
            ChosenLongitude = longitude;
        }
        public int NearestNumberOfScooters { get; }
        public int Radius { get; }
        public double ChosenLatitude { get; }
        public double ChosenLongitude { get; }
    }
}