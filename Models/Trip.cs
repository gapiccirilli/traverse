using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using Traverse.Models.Dto;


namespace Traverse.Models
{
    [Table("trips", Schema = "core")]
    public class Trip
    {
        public long Id { get; set; }
        public required string TripName { get; set; }
        public DateTimeOffset TripStart { get; set; }
        [NotMapped]
        public TimeZoneInfo TripStartTimezoneObj { get; set; }
        public string TripStartTimezone { get; set; }
        public DateTimeOffset TripEnd { get; set; }
        [NotMapped]
        public TimeZoneInfo TripEndTimezoneObj { get; set; }
        public string TripEndTimezone { get; set; }
        public string DepartureLocation { get; set; }
        public Point DepartureCoordinates { get; set; }
        public required string ArrivalLocation { get; set; }
        public required Point ArrivalCoordinates { get; set; }

        public Trip()
        {
            this.TripName = string.Empty;
            this.ArrivalLocation = string.Empty;
            this.ArrivalCoordinates = new Point(0, 0);
            this.TripStartTimezoneObj = TimeZoneInfo.Local;
            this.TripStartTimezone = TimeZoneInfo.Local.StandardName;
            this.DepartureLocation = string.Empty;
            this.DepartureCoordinates = new Point(0, 0);
            this.TripEndTimezoneObj = TimeZoneInfo.Local;
            this.TripEndTimezone = TimeZoneInfo.Local.StandardName;
        }
    }
}
