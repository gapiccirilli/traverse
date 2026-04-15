using NetTopologySuite.Geometries;
using Traverse.Models;
using Traverse.Models.Dto;

namespace Traverse.Utility.Impl
{
    public class PostMapper : IMapper<Post, PostDto>
    {
        public static PostDto MapFrom(Post input)
        {
            return new PostDto  
            {
                Id = input.Id,
                Title = input.Title,
                Content = input.Content,
                EventId = input.EventId,
            };
        }

        public static Post MapTo(PostDto input)
        {
            return new Post
            {
                Id = input.Id,
                Title = input.Title,
                Content = input.Content,
                EventId = input.EventId,
            };
        }
    }
}