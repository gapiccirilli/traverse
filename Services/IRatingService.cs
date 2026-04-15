using Traverse.Models;

namespace Traverse.Services
{
    public interface IRatingService
    {
        Task<Rating> GetRatingByIdAsync(long eventId);
        Task<Rating> CreateRatingAsync(long eventId, Rating rating);
        Task<Rating> UpdateRatingAsync(long eventId, long ratingId, Rating rating);
    }
}