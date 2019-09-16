using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using W2V.Posts.API.Domain.Models;
using W2V.Posts.API.Domain.Services;

namespace W2V.Posts.API.Controllers
{
    [Route("/api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> GetTopPosts()
        {
            var posts = await _postService.GetTopPosts();

            return posts;
        }


        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post p)
        {
            var result = await _postService.CreatePost(p);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Post);
        }

        [HttpPut]
        public async Task<IActionResult> IncrementUpVotes(long postId)
        {
            var result = await _postService.IncrementUpVotes(postId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Post);
        }

        [HttpPut]
        public async Task<IActionResult> IncrementDownVotes(long postId)
        {
            var result = await _postService.IncrementDownVotes(postId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Post);
        }

        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(long postId)
        {
            var result = await _postService.DeletePost(postId);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.Post);
        }
    }
}