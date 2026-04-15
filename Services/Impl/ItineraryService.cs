using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Repository;
using Traverse.Repository.Impl;
using Traverse.Utility.Impl;

namespace Traverse.Services.Impl
{
    internal class ItineraryService(IItineraryRepository itineraryRepository) : IItineraryService
    {
        private readonly IItineraryRepository _itineraryRepository = itineraryRepository;

        public async Task<ItineraryDto> CreateItineraryAsync(long tripId, ItineraryDto itinerary)
        {
            itinerary.TripId = tripId;
            return ItineraryMapper.MapFrom(await _itineraryRepository.CreateItineraryAsync(ItineraryMapper.MapTo(itinerary)));
        }

        public async Task DeleteItineraryAsync(long tripId, long itineraryId)
        {
            await _itineraryRepository.DeleteItineraryAsync(tripId, itineraryId);
        }

        public async Task<List<ItineraryDto>> GetItinerariesAsync(long tripId)
        {
            var itineraries = await _itineraryRepository.GetItinerariesAsync(tripId);
            return itineraries.Select(ItineraryMapper.MapFrom).ToList();
        }

        public async Task<ItineraryDto> GetItineraryByIdAsync(long tripId, long itineraryId)
        {
            var itinerary = await _itineraryRepository.GetItineraryByIdAsync(tripId, itineraryId);
            return ItineraryMapper.MapFrom(itinerary);
        }
        

        public async Task<ItineraryDto> UpdateItineraryAsync(long tripId, long itineraryId, ItineraryDto itinerary)
        {
            return ItineraryMapper.MapFrom(await _itineraryRepository.UpdateItineraryAsync(tripId, itineraryId, ItineraryMapper.MapTo(itinerary)));
        }
    }
}