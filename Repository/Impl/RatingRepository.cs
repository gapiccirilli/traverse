using Microsoft.EntityFrameworkCore;
using Traverse.DbContexts;
using Traverse.Models;

namespace Traverse.Repository.Impl
{
    public class RatingRepository(CoreDbContext coreContext) : IRatingRepository
    {
        private readonly CoreDbContext _coreContext = coreContext;
        public async Task<Rating> CreateRatingAsync(Rating rating)
        {
            _coreContext.Add(rating);
            await _coreContext.SaveChangesAsync();
            return rating;
        }

        public async Task<Rating> GetRatingByIdAsync(long eventId)
        {
            return await _coreContext.Ratings
                        .Where(r => r.EventId == eventId)
                        .SingleOrDefaultAsync() ?? throw new KeyNotFoundException($"Rating for Event ID {eventId} not found.");
        }

        public async Task<Rating> UpdateRatingAsync(long eventId, long ratingId, Rating rating)
        {
            int rows = await _coreContext.Ratings
                .Where(r => r.Id == ratingId && r.EventId == eventId)
                .ExecuteUpdateAsync(s => s.SetProperty(r => r.Value, rating.Value));

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Rating with ID {ratingId} and Event ID {eventId} not found.");
            }

            return await _coreContext.Ratings.FindAsync(ratingId) ?? throw new KeyNotFoundException($"Rating with ID {ratingId} not found after update.");
        }
    }
}