using Microsoft.EntityFrameworkCore;
using Traverse.DbContexts;
using Traverse.Models;

namespace Traverse.Repository.Impl
{
    public class PostRepository(CoreDbContext coreContext) : IPostRepository
    {
        private readonly CoreDbContext _coreContext = coreContext;

        public async Task<Post> CreatePostAsync(Post post)
        {
            _coreContext.Add(post);
            await _coreContext.SaveChangesAsync();
            return post;
        }

        public async Task DeletePostAsync(long eventId, long postId)
        {
            int rows = await _coreContext.Posts
                .Where(p => p.Id == postId && p.EventId == eventId)
                .ExecuteDeleteAsync<Post>();

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Post with ID {postId} and Event ID {eventId} not found.");
            }
        }

        public async Task<Post> GetPostByIdAsync(long eventId, long postId)
        {
            return await _coreContext.Posts
                        .Where(p => p.Id == postId && p.EventId == eventId)
                        .SingleOrDefaultAsync() ?? throw new KeyNotFoundException($"Post with ID {postId} and Event ID {eventId} not found.");
        }

        public async Task<IEnumerable<Post>> GetPostsAsync(long eventId)
        {
            return await _coreContext.Posts
                .Where(p => p.EventId == eventId)
                .ToListAsync();
        }

        public async Task<Post> UpdatePostAsync(long eventId, long postId, Post post)
        {
            int rows = await _coreContext.Posts
                .Where(p => p.Id == postId && p.EventId == eventId)
                .ExecuteUpdateAsync(s => s.SetProperty(p => p.Title, post.Title)
                                        .SetProperty(p => p.Content, post.Content));

            if (rows == 0)
            {
                throw new KeyNotFoundException($"Post with ID {postId} and Event ID {eventId} not found.");
            }

            return await _coreContext.Posts.FindAsync(postId) ?? throw new KeyNotFoundException($"Post with ID {postId} not found after update.");
        }
    }
}