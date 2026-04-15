using Traverse.Models;

namespace Traverse.Repository
{
    public interface IItineraryRepository
    {
        Task<List<Itinerary>> GetItinerariesAsync(long tripId);

        Task<Itinerary> GetItineraryByIdAsync(long tripId, long itineraryId);

        Task<Itinerary> CreateItineraryAsync(Itinerary itinerary);

        Task<Itinerary> UpdateItineraryAsync(long tripId, long itineraryId, Itinerary itinerary);

        Task DeleteItineraryAsync(long tripId, long itineraryId);
    }
}