using Traverse.Models;

namespace Traverse.Repository
{
    public interface IRatingRepository
    {
        Task<Rating> GetRatingByIdAsync(long eventId);
        Task<Rating> CreateRatingAsync(Rating rating);
        Task<Rating> UpdateRatingAsync(long eventId, long ratingId, Rating rating);
    }
}