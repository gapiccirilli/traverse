using Traverse.Models;
using Traverse.Models.Dto;

namespace Traverse.Services
{
    public interface ITripService
    {
        Task<TripDto> CreateTripAsync(TripDto trip);
        Task<TripDto> GetTripByIdAsync(long id);
        Task<IEnumerable<TripDto>> GetAllTripsAsync();
        Task<TripDto> UpdateTripAsync(long tripId, TripDto trip);
        Task DeleteTripAsync(long tripId);
    }
}
