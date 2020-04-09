using System;

namespace Beam_intern.Scooter.CoordinatesHelper
{
    public static class CoordinatesDistanceExtensions
    {
        /**
         * This method is responsible for calculating the actual distance between two points.
         * As points are given in latitude and longitude,
         * traditional Cartesian calculations cannot be applied.
         * This method calculates the distance between two points and returns
         * the distance in METRES.
         */
        public static double GetDistance(double longitude, double latitude, double otherLongitude, double otherLatitude)
        {
            // Resulting distance is in metres!
            var d1 = latitude * (Math.PI / 180.0);
            var num1 = longitude * (Math.PI / 180.0);
            var d2 = otherLatitude * (Math.PI / 180.0);
            var num2 = otherLongitude * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}