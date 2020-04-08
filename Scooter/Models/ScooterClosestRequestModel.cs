namespace Beam_intern.Scooter.Models
{
    public class ScooterClosestRequestModel
    {
        public int NearestNumberOfScooters { get; set; }
        public int Radius { get; set; }
        public double ChosenLatitude { get; set; }
        public double ChosenLongitude { get; set; }
    }
}