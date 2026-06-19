using Hangfire;
using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Repository;
using Traverse.Services.Graph;
using Traverse.Services.Timezone;
using Traverse.Utility.Impl;

namespace Traverse.Services.Impl
{
    internal class EventService(IEventRepository eventRepository, ITimeZoneService timeZoneService, IEdgeService<EventDto, Transportation> transporationEdgeServce, IBackgroundJobClient etaJobClient) : IEventService
    {
        private readonly IEventRepository _eventRepository = eventRepository;
        private readonly ITimeZoneService _timeZoneService = timeZoneService;
        private readonly IEdgeService<EventDto, Transportation> _transporationEdgeServce = transporationEdgeServce;
        private readonly IBackgroundJobClient _etaJobClient = etaJobClient;

        public async Task<EventDto> CreateEventAsync(long itineraryId, EventDto newEvent)
        {
            newEvent.ItineraryId = itineraryId;
            
            newEvent.EventTimeZone = await _timeZoneService.GetTimeZoneAsync(newEvent.Coordinates.Latitude, newEvent.Coordinates.Longitude);

            Event? previousNodeEntity = await _eventRepository.GetMostRecentEventAsync(itineraryId);
            EventDto? previousNode = previousNodeEntity is not null ? EventMapper.MapFrom(previousNodeEntity) : null;

            newEvent.UserDefinedOrder = (short?)(previousNodeEntity is not null ? previousNodeEntity.UserDefinedOrder + 1 : 1);

            var savedEvent = EventMapper.MapFrom(await _eventRepository.CreateEventAsync(itineraryId, EventMapper.MapTo(newEvent)));

            // _etaJobClient.Enqueue<IEdgeService<EventDto, Transportation>>(service => service.BuildEdgesAsync(savedEvent, itineraryId));

            await _transporationEdgeServce.BuildEdgeAsync(savedEvent, previousNode, itineraryId);
            
            return savedEvent;
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

        public async Task<EventDto> GetMostRecentEventAsync(long itineraryId)
        {
            return EventMapper.MapFrom(await _eventRepository.GetMostRecentEventAsync(itineraryId));
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