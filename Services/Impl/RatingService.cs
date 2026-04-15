using Traverse.Models;
using Traverse.Repository;

namespace Traverse.Services.Impl
{
    internal class RatingService(IRatingRepository ratingRepository) : IRatingService
    {
        private readonly IRatingRepository _ratingRepository = ratingRepository;

        public async Task<Rating> CreateRatingAsync(long eventId,Rating rating)
        {
            rating.EventId = eventId;
            return await _ratingRepository.CreateRatingAsync(rating);
        }

        public async Task<Rating> GetRatingByIdAsync(long eventId)
        {
            return await _ratingRepository.GetRatingByIdAsync(eventId);
        }

        public async Task<Rating> UpdateRatingAsync(long eventId, long ratingId, Rating rating)
        {
            return await _ratingRepository.UpdateRatingAsync(eventId, ratingId, rating);
        }
    }
}