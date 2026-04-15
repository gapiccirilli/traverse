using NetTopologySuite.Geometries;

namespace Traverse.Models
{
    public class EventDto
    {
        public long Id { get; set; }
        public string EventName { get; set; }
        public string Location { get; set; }
        public Records.Coordinate Coordinates { get; set; }
        public DateTime EventDate { get; set; }
        public string EventTimeZone { get; set; }
        public bool IsTransportation { get; set; }
        public short? Duration { get; set; }
        public long ItineraryId { get; set; }

        public EventDto()
        {
            this.EventName = string.Empty;
            this.Location = string.Empty;
            this.Coordinates = new Records.Coordinate(0, 0);
            this.EventTimeZone = TimeZoneInfo.Local.Id;
;
        }
        public EventDto(string eventName, string location, Records.Coordinate coordinates, DateTime eventDate, string timeZone)
        {
            this.EventName = eventName;
            this.Location = location;
            this.Coordinates = coordinates;
            this.EventDate = eventDate;
            this.EventTimeZone = timeZone;
        }
        public EventDto(int id,string eventName, string location, Records.Coordinate coordinates, DateTime eventDate, string timeZone, short duration, long itineraryId)
        {
            this.Id = id;
            this.EventName = eventName;
            this.Location = location;
            this.Coordinates = coordinates;
            this.EventDate = eventDate;
            this.Duration = duration;
            this.ItineraryId = itineraryId;
            this.EventTimeZone = timeZone;
        }
    }
}