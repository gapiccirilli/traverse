using Microsoft.EntityFrameworkCore;
using Traverse.DbContexts;
using Traverse.Models;

namespace Traverse.Repository.Impl
{
    public class EventRepository(CoreDbContext coreContext) : IEventRepository
    {
        private readonly CoreDbContext _coreContext = coreContext;
        public async Task<Event> CreateEventAsync(long itineraryId, Event newEvent)
        {
            _coreContext.Add(newEvent);
            await _coreContext.SaveChangesAsync();
            return newEvent;
        }

        public async Task DeleteEventAsync(long itineraryId, long eventId)
        {
            int rows = await _coreContext.Events
                .Where(e => e.Id == eventId && e.ItineraryId == itineraryId)
                .ExecuteDeleteAsync<Event>();

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Event with ID {eventId} and Itinerary ID {itineraryId} not found.");
            }
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync(long itineraryId)
        {
            return await _coreContext.Events
                .Where(e => e.ItineraryId == itineraryId)
                .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(long itineraryId, long eventId)
        {
            return await _coreContext.Events
                        .Where(e => e.Id == eventId && e.ItineraryId == itineraryId)
                        .SingleOrDefaultAsync() ?? throw new KeyNotFoundException($"Event with ID {eventId} and Itinerary ID {itineraryId} not found.");
        }

        public async Task<Event> PatchEventAsync(long itineraryId, long eventId, Event updatedEvent)
        {
            throw new NotImplementedException();
        }

        public async Task<Event> UpdateEventAsync(long itineraryId, long eventId, Event updatedEvent)
        {
            int rows = await _coreContext.Events
                .Where(e => e.Id == eventId && e.ItineraryId == itineraryId)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.EventName, updatedEvent.EventName)
                                        .SetProperty(e => e.Location, updatedEvent.Location)
                                        .SetProperty(e => e.Coordinates, updatedEvent.Coordinates)
                                        .SetProperty(e => e.EventDate, updatedEvent.EventDate)
                                        .SetProperty(e => e.Duration, updatedEvent.Duration)
                                        .SetProperty(e => e.IsTransportation, updatedEvent.IsTransportation));

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Event with ID {eventId} and Itinerary ID {itineraryId} not found.");
            }

            return await _coreContext.Events.FindAsync(eventId) ?? throw new KeyNotFoundException($"Event with ID {eventId} not found after update.");
        }
    }
}