using Microsoft.EntityFrameworkCore;
using Traverse.DbContexts;
using Traverse.Models;

namespace Traverse.Repository.Impl
{
    public class ItineraryRepository(CoreDbContext coreContext) : IItineraryRepository
    {
        private readonly CoreDbContext _coreContext = coreContext;

        public async Task<Itinerary> CreateItineraryAsync(Itinerary itinerary)
        {
            _coreContext.Add(itinerary);
            await _coreContext.SaveChangesAsync();
            return itinerary;
        }

        public async Task DeleteItineraryAsync(long tripId, long itineraryId)
        {
            int rows = await _coreContext.Itineraries
                .Where(i => i.Id == itineraryId && i.TripId == tripId)
                .ExecuteDeleteAsync<Itinerary>();

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Itinerary with ID {itineraryId} not found.");
            }
        }

        public async Task<List<Itinerary>> GetItinerariesAsync(long tripId)
        {
            return await _coreContext.Itineraries
                .Where(i => i.TripId == tripId)
                .ToListAsync();
        }

        public async Task<Itinerary> GetItineraryByIdAsync(long tripId, long itineraryId)
        {
            return await _coreContext.Itineraries
                        .Where(i => i.Id == itineraryId && i.TripId == tripId)
                        .SingleOrDefaultAsync() ?? throw new KeyNotFoundException($"Itinerary with ID {itineraryId} and Trip with ID {tripId} not found.");
        }

        public async Task<Itinerary> UpdateItineraryAsync(long tripId, long itineraryId, Itinerary itinerary)
        {
            int rows = await _coreContext.Itineraries
                .Where(i => i.Id == itineraryId && i.TripId == tripId)
                .ExecuteUpdateAsync(s => s.SetProperty(i => i.ItineraryName, itinerary.ItineraryName));

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Itinerary with ID {itineraryId} and Trip with ID {tripId} not found.");
            }

            return await _coreContext.Itineraries.FindAsync(itineraryId) ?? throw new KeyNotFoundException($"Itinerary with ID {itineraryId} not found after update.");
        }
    }
}