using Traverse.Models;

namespace Traverse.Repository
{
    public interface ITripRepository
    {
        Task<Trip> CreateTripAsync(Trip trip);
        Task<Trip> GetTripByIdAsync(long id);
        Task<IEnumerable<Trip>> GetAllTripsAsync();
        Task<Trip> UpdateTripAsync(long tripId, Trip trip);
        Task DeleteTripAsync(long tripId);
    }
}
