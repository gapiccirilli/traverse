using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;
using Traverse.Models.Records;

namespace Traverse.Models
{
    [Table("events", Schema = "core")]
    public class Event
    {
        public long Id { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public Point Coordinates { get; set; }
        public DateTime EventDate { get; set; }
        [NotMapped]
        public TimeZoneInfo TimeZoneObj { get; set; }
        public string EventTimeZone { get; set; }
        public short? Duration { get; set; }
        public bool IsTransportation { get; set; } = false;
        public long ItineraryId { get; set; }

        public Event()
        {
            this.EventName = string.Empty;
            this.Location = string.Empty;
            this.Coordinates = new Point(0, 0);
            this.TimeZoneObj = TimeZoneInfo.Local;
            this.EventTimeZone = TimeZoneInfo.Local.StandardName;
        }
    }
}