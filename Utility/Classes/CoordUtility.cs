using Traverse.Models.Records;

namespace Traverse.Utility.Classes
{
    public class CoordUtility
    {
        public static Coordinate ParseCoordinateString(string coordinate)
        {
            string[] coordinateSplit = coordinate.Replace(" ", "").Split(",");

            Coordinate coord = new()
            {
                Latitude = Convert.ToDouble(coordinateSplit[0]),
                Longitude = Convert.ToDouble(coordinateSplit[1])
            };


            return coord;
        }
    }
}