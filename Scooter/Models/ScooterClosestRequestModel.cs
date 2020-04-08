namespace Beam_intern.Scooter.Models
{
    public class ScooterClosestRequestModel
    {
        public int NearestNumberOfScooters { get; }
        public int Radius { get; }
        public double ChosenLatitude { get; }
        public double ChosenLongitude { get; }
    }
}