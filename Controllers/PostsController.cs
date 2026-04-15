using Microsoft.AspNetCore.Mvc;
using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Services;

namespace Traverse.Controllers
{
    [ApiController]
    [Route("api/events/{eventId}/[controller]")]
    public class PostsController(IPostService postService) : ControllerBase
    {
        private readonly IPostService _postService = postService;

        [HttpGet]
        public async Task<IActionResult> GetPosts(long eventId)
        {
            return Ok(await _postService.GetPostsAsync(eventId));
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> GetPostById(long eventId,long postId)
        {
            return Ok(await _postService.GetPostByIdAsync(eventId, postId));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(long eventId, [FromBody] PostDto post)
        {
            return CreatedAtAction(nameof(GetPostById), new { eventId = post.EventId, postId = post.Id }, await _postService.CreatePostAsync(eventId, post));
        }

        [HttpPut("{postId}")]
        public async Task<IActionResult> UpdatePost(long eventId,long postId, [FromBody] PostDto post)
        {
            return Ok(await _postService.UpdatePostAsync(eventId, postId, post));
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(long eventId,long postId)
        {
            await _postService.DeletePostAsync(eventId, postId);
            return NoContent();
        }
    }
}

