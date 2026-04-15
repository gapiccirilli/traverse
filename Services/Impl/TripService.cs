using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Repository;
using Traverse.Services.Timezone;
using Traverse.Utility.Impl;

namespace Traverse.Services.Impl
{
    internal class TripService(ITripRepository tripRepository, ITimeZoneService timezoneService) : ITripService
    {
        private readonly ITripRepository _tripRepository = tripRepository;
        private readonly ITimeZoneService _timezoneService = timezoneService;

        public async Task<TripDto> CreateTripAsync(TripDto trip)
        {
            trip.TripStartTimezone = await _timezoneService.GetTimeZoneAsync(trip.ArrivalCoordinates.Latitude, trip.ArrivalCoordinates.Longitude);
            trip.TripEndTimezone = await _timezoneService.GetTimeZoneAsync(trip.DepartureCoordinates.Latitude, trip.DepartureCoordinates.Longitude);

            var entity = await _tripRepository.CreateTripAsync(TripMapper.MapTo(trip));
            return TripMapper.MapFrom(entity);
        }

        public async Task DeleteTripAsync(long tripId)
        {
            await _tripRepository.DeleteTripAsync(tripId);
        }

        public async Task<IEnumerable<TripDto>> GetAllTripsAsync()
        {
            var trips = await _tripRepository.GetAllTripsAsync();
            return trips.Select(TripMapper.MapFrom);
        }

        public async Task<TripDto> GetTripByIdAsync(long id)
        {
            var entity = await _tripRepository.GetTripByIdAsync(id);
            return TripMapper.MapFrom(entity);
        }

        public async Task<TripDto> UpdateTripAsync(long tripId, TripDto trip)
        {
            var entity = await _tripRepository.UpdateTripAsync(tripId, TripMapper.MapTo(trip));
            return TripMapper.MapFrom(entity);
        }
    }
}