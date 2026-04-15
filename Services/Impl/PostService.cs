using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Repository;
using Traverse.Utility.Impl;

namespace Traverse.Services.Impl
{
    internal class PostService(IPostRepository postRepository) : IPostService
    {
        private readonly IPostRepository _postRepository = postRepository;

        public async Task<PostDto> CreatePostAsync(long eventId, PostDto post)
        {
            post.EventId = eventId;
            return PostMapper.MapFrom(await _postRepository.CreatePostAsync(PostMapper.MapTo(post)));
        }

        public async Task DeletePostAsync(long eventId, long postId)
        {
            await _postRepository.DeletePostAsync(eventId, postId);
        }

        public async Task<PostDto> GetPostByIdAsync(long eventId, long postId)
        {
            return PostMapper.MapFrom(await _postRepository.GetPostByIdAsync(eventId, postId));
        }

        public async Task<IEnumerable<PostDto>> GetPostsAsync(long eventId)
        {
            var entity = await _postRepository.GetPostsAsync(eventId);
            return entity.Select(PostMapper.MapFrom);
        }

        public async Task<PostDto> UpdatePostAsync(long eventId, long postId, PostDto post)
        {
            return PostMapper.MapFrom(await _postRepository.UpdatePostAsync(eventId, postId, PostMapper.MapTo(post)));
        }
    }
}