using System.Collections.Generic;
using System.Threading.Tasks;
using W2V.Posts.API.Domain.Models;
using W2V.Posts.API.Domain.Repositories;

namespace W2V.Posts.API.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IPostsRepository _postsRepository;

        public PostService(IPostsRepository postsRepository)
        {
            _postsRepository = postsRepository;
        }

        public async Task<IEnumerable<Post>> GetTopPosts(int numOfPosts)
        {
            return await _postsRepository.GetTopPosts(numOfPosts);
        }

        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await _postsRepository.GetAllPosts();
        }

        public async Task CreatePost(Post post)
        {
                await _postsRepository.CreatePost(post);
        }

        public async Task IncrementUpVotes(long postId)
        {
                await _postsRepository.IncrementUpVotes(postId);
        }

        public async Task IncrementDownVotes(long postId)
        {
            await _postsRepository.IncrementDownVotes(postId);
        }

        public async Task DeletePost(long postId)
        {
            await _postsRepository.DeletePost(postId);
        }

        public async Task UpdatePostText(long postId, string text)
        {
            await _postsRepository.UpdatePostText(postId, text);
        }
    }
}