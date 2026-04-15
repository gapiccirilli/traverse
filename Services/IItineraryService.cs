using Traverse.Models.Dto;

namespace Traverse.Services
{
    public interface IItineraryService
    {
        Task<List<ItineraryDto>> GetItinerariesAsync(long tripId);

        Task<ItineraryDto> GetItineraryByIdAsync(long tripId, long itineraryId);

        Task<ItineraryDto> CreateItineraryAsync(long tripId, ItineraryDto itinerary);   
        Task<ItineraryDto> UpdateItineraryAsync(long tripId, long itineraryId, ItineraryDto itinerary);

        Task DeleteItineraryAsync(long tripId, long itineraryId);
    }
}