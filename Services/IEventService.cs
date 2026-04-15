using Traverse.Models;

namespace Traverse.Services
{
    public interface IEventService
    {
        Task<EventDto> GetEventByIdAsync(long itineraryId, long eventId);
        Task<IEnumerable<EventDto>> GetAllEventsAsync(long itineraryId);
        Task<EventDto> CreateEventAsync(long itineraryId, EventDto newEvent);
        Task<EventDto> UpdateEventAsync(long itineraryId, long eventId, EventDto updatedEvent);
        Task<EventDto> PatchEventAsync(long itineraryId, long eventId, EventDto updatedEvent);
        Task DeleteEventAsync(long itineraryId, long eventId);
    }
}