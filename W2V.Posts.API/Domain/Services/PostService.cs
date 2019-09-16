using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using W2V.Posts.API.Domain.Models;
using W2V.Posts.API.Domain.Repositories;
using W2V.Posts.API.Domain.Services.Communication;

namespace W2V.Posts.API.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IPostsRepository _postsRepository;

        public PostService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<IEnumerable<Post>> GetTopPosts()
        {
            return await _postsRepository.GetTopPosts();
        }

        public async Task<PostResponse> CreatePost(Post post)
        {
            try
            {
                await _postsRepository.CreatePost(post);

                return new PostResponse(post);
            }
            catch (Exception ex)
            {
                return new PostResponse($"An error occurred when creating Post: {ex.Message}");
            }
        }

        public async Task<PostResponse> IncrementUpVotes(long postId)
        {
            try
            {
                Post existingPost = await _postsRepository.GetPostById(postId);

                if (existingPost == null)
                {
                    return new PostResponse($"Post with id={postId} not found.");
                }

                await _postsRepository.IncrementUpVotes(postId);

                existingPost.UpVotes++;
                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                return new PostResponse($"An error occurred when increment UpVotes: {ex.Message}");
            }
        }

        public async Task<PostResponse> IncrementDownVotes(long postId)
        {
            try
            {
                Post existingPost = await _postsRepository.GetPostById(postId);

                if (existingPost == null)
                {
                    return new PostResponse($"Post with id={postId} not found.");
                }

                await _postsRepository.IncrementDownVotes(postId);

                existingPost.DownVotes++;
                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                return new PostResponse($"An error occurred when increment DownVotes: {ex.Message}");
            }
        }

        public async Task<PostResponse> DeletePost(long postId)
        {
            try
            {
                Post existingPost = await _postsRepository.GetPostById(postId);

                if (existingPost == null)
                {
                    return new PostResponse($"Post with id={postId} not found.");
                }

                await _postsRepository.DeletePost(postId);

                return new PostResponse(existingPost);
            }
            catch (Exception ex)
            {
                return new PostResponse($"An error occurred when Delete Post: {ex.Message}");
            }
        }
    }
}
