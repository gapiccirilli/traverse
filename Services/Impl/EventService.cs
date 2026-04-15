using Traverse.Models;
using Traverse.Repository;
using Traverse.Services.Timezone;
using Traverse.Utility.Impl;

namespace Traverse.Services.Impl
{
    internal class EventService(IEventRepository eventRepository, ITimeZoneService timeZoneService) : IEventService
    {
        private readonly IEventRepository _eventRepository = eventRepository;
        private readonly ITimeZoneService _timeZoneService = timeZoneService;

        public async Task<EventDto> CreateEventAsync(long itineraryId, EventDto newEvent)
        {
            newEvent.ItineraryId = itineraryId;
            
            newEvent.EventTimeZone = await _timeZoneService.GetTimeZoneAsync(newEvent.Coordinates.Latitude, newEvent.Coordinates.Longitude);
            
            return EventMapper.MapFrom(await _eventRepository.CreateEventAsync(itineraryId, EventMapper.MapTo(newEvent)));
        }

        public async Task DeleteEventAsync(long itineraryId, long eventId)
        {
            await _eventRepository.DeleteEventAsync(itineraryId, eventId);
        }

        public async Task<IEnumerable<EventDto>> GetAllEventsAsync(long itineraryId)
        {
            var events = await _eventRepository.GetAllEventsAsync(itineraryId);
            return events.Select(EventMapper.MapFrom);
        }

        public async Task<EventDto> GetEventByIdAsync(long itineraryId, long eventId)
        {
            return EventMapper.MapFrom(await _eventRepository.GetEventByIdAsync(itineraryId, eventId));
        }

        public async Task<EventDto> PatchEventAsync(long itineraryId, long eventId, EventDto updatedEvent)
        {
            throw new NotImplementedException();
        }

        public async Task<EventDto> UpdateEventAsync(long itineraryId, long eventId, EventDto updatedEvent)
        {
            return EventMapper.MapFrom(await _eventRepository.UpdateEventAsync(itineraryId, eventId, EventMapper.MapTo(updatedEvent)));
        }
    }
}