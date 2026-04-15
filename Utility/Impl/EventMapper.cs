using NetTopologySuite.Geometries;
using Traverse.Models;

namespace Traverse.Utility.Impl
{
    public class EventMapper : IMapper<Event, EventDto>
    {
        public static EventDto MapFrom(Event input)
        {
            return new EventDto
            {
                Id = input.Id,
                EventName = input.EventName,
                Location = input.Location,
                Coordinates = new Models.Records.Coordinate(input.Coordinates.X, input.Coordinates.Y),
                EventDate = input.EventDate,
                IsTransportation = input.IsTransportation,
                Duration = input.Duration,
                ItineraryId = input.ItineraryId
            };
        }

        public static Event MapTo(EventDto input)
        {
            return new Event
            {
                Id = input.Id,
                EventName = input.EventName,
                Location = input.Location,
                Coordinates = new Point(input.Coordinates.Latitude, input.Coordinates.Longitude),
                EventDate = input.EventDate.ToUniversalTime(),
                IsTransportation = input.IsTransportation,
                Duration = input.Duration,
                ItineraryId = input.ItineraryId
            };
        }
    }
}