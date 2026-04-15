using NetTopologySuite.Geometries;
using Traverse.Models;

namespace Traverse.Models.Dto
{
    public class TripDto
    {
        public long Id { get; set; }
        public string TripName { get; set; }
        public DateTimeOffset TripStart { get; set; }
        public string TripStartTimezone { get; set; }
        public DateTimeOffset TripEnd { get; set; }
        public string TripEndTimezone { get; set; }
        public string DepartureLocation { get; set; }
        public Records.Coordinate DepartureCoordinates { get; set; }
        public string ArrivalLocation { get; set; }
        public Records.Coordinate ArrivalCoordinates { get; set; }

        public TripDto()
        {
            this.TripName = string.Empty;
            this.ArrivalLocation = string.Empty;
            this.ArrivalCoordinates = new Records.Coordinate(0, 0);
            this.DepartureLocation = string.Empty;
            this.DepartureCoordinates = new Records.Coordinate(0, 0);
            this.TripStartTimezone = TimeZoneInfo.Local.StandardName;
            this.TripEndTimezone = TimeZoneInfo.Local.StandardName;
        }
    }
}
