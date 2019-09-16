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
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            //IEnumerable<Post> posts = new List<Post>()
            //{
            //    new Post()
            //    {
            //        CreationTime = DateTime.UtcNow,
            //        Id = 1,
            //        Text = "1st test post",
            //        DownVotes = 5,
            //        UpVotes = 50
            //    },
            //    new Post()
            //    {
            //        CreationTime = DateTime.UtcNow,
            //        Id = 2,
            //        Text = "2nd test post",
            //        DownVotes = 25,
            //        UpVotes = 100
            //    },
            //    new Post()
            //    {
            //        CreationTime = DateTime.UtcNow,
            //        Id = 3,
            //        Text = "3td test post",
            //        DownVotes = 52,
            //        UpVotes = 300
            //    }
            //};
            var posts = await _postService.GetAllPosts();
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