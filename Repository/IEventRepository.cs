using Traverse.Models;

namespace Traverse.Repository
{
    public interface IEventRepository
    {
        Task<Event> GetEventByIdAsync(long itineraryId, long eventId);
        Task<IEnumerable<Event>> GetAllEventsAsync(long itineraryId);
        Task<Event> CreateEventAsync(long itineraryId, Event newEvent);
        Task<Event> UpdateEventAsync(long itineraryId, long eventId, Event updatedEvent);
        Task<Event> PatchEventAsync(long itineraryId, long eventId, Event updatedEvent);
        Task DeleteEventAsync(long itineraryId, long eventId);
    }
}