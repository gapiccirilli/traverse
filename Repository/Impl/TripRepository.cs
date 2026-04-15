using Microsoft.EntityFrameworkCore;
using Traverse.DbContexts;
using Traverse.Models;

namespace Traverse.Repository.Impl
{
    public class TripRepository(CoreDbContext coreContext) : ITripRepository
    {
        private readonly CoreDbContext _coreContext = coreContext;

        public async Task<Trip> CreateTripAsync(Trip trip)
        {
            _coreContext.Add(trip);
            await _coreContext.SaveChangesAsync();
            return trip;
        }

        public async Task DeleteTripAsync(long tripId)
        {
            int rows = await _coreContext.Trips
                .Where(t => t.Id == tripId)
                .ExecuteDeleteAsync<Trip>();

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Trip with ID {tripId} not found.");
            }
        }

        public async Task<IEnumerable<Trip>> GetAllTripsAsync()
        {
            return await _coreContext.Trips.ToListAsync();
        }

        public async Task<Trip> GetTripByIdAsync(long id)
        {
            return await _coreContext.Trips.Where(t => t.Id == id).SingleOrDefaultAsync()
                ?? throw new KeyNotFoundException($"Trip with ID {id} not found.");
        }

        public async Task<Trip> UpdateTripAsync(long tripId, Trip trip)
        {

            int rows = await _coreContext.Trips
                .Where(t => t.Id == tripId)
                .ExecuteUpdateAsync(s => s.SetProperty(t => t.TripName, trip.TripName)
                    .SetProperty(t => t.TripStart, trip.TripStart)
                    .SetProperty(t => t.TripEnd, trip.TripEnd)
                    .SetProperty(t => t.DepartureLocation, trip.DepartureLocation)
                    .SetProperty(t => t.DepartureCoordinates, trip.DepartureCoordinates)
                    .SetProperty(t => t.ArrivalLocation, trip.ArrivalLocation)
                    .SetProperty(t => t.ArrivalCoordinates, trip.ArrivalCoordinates)
                    .SetProperty(t => t.TripStartTimezone, trip.TripStartTimezone)
                );

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Trip with ID {tripId} not found.");
            }

            return await _coreContext.Trips.FindAsync(tripId) ?? throw new KeyNotFoundException($"Trip with ID {tripId} not found after update.");
        }
    }
}