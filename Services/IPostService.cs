using Traverse.Models;
using Traverse.Models.Dto;

namespace Traverse.Services
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetPostsAsync(long eventId);
        Task<PostDto> GetPostByIdAsync(long eventId, long postId);
        Task<PostDto> CreatePostAsync(long eventId, PostDto post);
        Task<PostDto> UpdatePostAsync(long eventId, long postId, PostDto post);
        Task DeletePostAsync(long eventId, long postId);
    }
}