using Traverse.Models;

namespace Traverse.Repository
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetPostsAsync(long eventId);
        Task<Post> GetPostByIdAsync(long eventId, long postId);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(long eventId, long postId, Post post);
        Task DeletePostAsync(long eventId, long postId);
    }
}