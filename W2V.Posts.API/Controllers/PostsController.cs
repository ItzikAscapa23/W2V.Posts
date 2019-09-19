using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using W2V.Posts.API.Domain.Models;
using W2V.Posts.API.Domain.Services;
using W2V.Posts.API.Domain.Services.Communication;

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

        [HttpGet("GetAllPosts")]
        public async Task<PostsResponse> GetAllPosts()
        {
            string x = nameof(GetAllPosts);
            PostsResponse response;
            try
            {
                IEnumerable<Post> posts = await _postService.GetAllPosts();

                if (posts == null)
                {
                    return new PostsResponse("No post was found.");
                }

                return new PostsResponse(posts);
            }
            catch (Exception ex)
            {
                //Todo: Write exception details to log
                response = new PostsResponse($"An error was occurred on {nameof(GetAllPosts)}, please call admin.");
            }

            return response;
        }


        [HttpGet("TopPosts")]
        public async Task<PostsResponse> TopPosts(int numOfPosts)
        {
            PostsResponse response;
            try
            {
                IEnumerable<Post> posts = await _postService.GetTopPosts(numOfPosts);

                if (posts == null)
                {
                    return new PostsResponse("No post was found.");
                }

                return new PostsResponse(posts);
            }
            catch (Exception ex)
            {
                //Todo: Write exception details to log
                response = new PostsResponse($"An error was occurred on {nameof(TopPosts)}, please call admin.");
            }

            return response;
        }


        [HttpPost("CreatePost")]
        public async Task<PostsResponse> CreatePost([FromBody] Post p)
        {
            PostsResponse response;
            try
            {
                await _postService.CreatePost(p);

                return new PostsResponse(true);
            }
            catch (Exception e)
            {
                //Todo: Write exception details to log
                response = new PostsResponse($"An error was occurred on {nameof(CreatePost)}, please call admin.");
            }

            return response;
        }

        [HttpPut("UpdatePostText")]
        public async Task<PostsResponse> UpdatePostText(long postId, [FromBody] string text)
        {
            PostsResponse response;
            try
            {
                await _postService.UpdatePostText(postId, text);

                return new PostsResponse(true);
            }
            catch (Exception e)
            {
                //Todo: Write exception details to log
                response = new PostsResponse($"An error was occurred on {nameof(UpdatePostText)}, please call admin.");
            }

            return response;
        }
        [HttpPut("IncrementUpVotes")]
        public async Task<PostsResponse> IncrementUpVotes(long postId)
        {
            PostsResponse response;
            try
            {
                await _postService.IncrementUpVotes(postId);

                return new PostsResponse(true);
            }
            catch (Exception e)
            {
                //Todo: Write exception details to log
                response = new PostsResponse($"An error was occurred on {nameof(IncrementUpVotes)}, please call admin.");
            }

            return response;
        }

        [HttpPut("IncrementDownVotes")]
        public async Task<PostsResponse> IncrementDownVotes(long postId)
        {
            PostsResponse response;
            try
            {
                await _postService.IncrementDownVotes(postId);

                return new PostsResponse(true);
            }
            catch (Exception e)
            {
                //Todo: Write exception details to log
                response = new PostsResponse($"An error was occurred on {nameof(IncrementDownVotes)}, please call admin.");
            }

            return response;
        }

        [HttpDelete("DeletePost")]
        public async Task<PostsResponse> DeletePost(long postId)
        {

            PostsResponse response;
            try
            {
                await _postService.DeletePost(postId);

                return new PostsResponse(true);
            }
            catch (Exception e)
            {
                //Todo: Write exception details to log
                response = new PostsResponse($"An error was occurred on {nameof(DeletePost)}, please call admin.");
            }

            return response;
        }
    }
}