using NetTopologySuite.Geometries;
using Traverse.Models;
using Traverse.Models.Dto;

namespace Traverse.Utility.Impl
{
    public class ItineraryMapper : IMapper<Itinerary, ItineraryDto>
    {
        public static ItineraryDto MapFrom(Itinerary input)
        {
            return new ItineraryDto
            {
                Id = input.Id,
                ItineraryName = input.ItineraryName,
                TripId = input.TripId
            };
        }

        public static Itinerary MapTo(ItineraryDto input)
        {
            return new Itinerary
            {
                Id = input.Id,
                ItineraryName = input.ItineraryName,
                TripId = input.TripId
            };
        }
    }
}