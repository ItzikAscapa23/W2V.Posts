using System.Collections.Generic;
using System.Threading.Tasks;
using W2V.Posts.API.Domain.Models;

namespace W2V.Posts.API.Domain.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetTopPosts(int numOfPosts);
        Task<IEnumerable<Post>> GetAllPosts();
        Task CreatePost(Post post);
        Task IncrementUpVotes(long postId);
        Task IncrementDownVotes(long postId);
        Task DeletePost(long postId);
        Task UpdatePostText(long postId, string text);
    }
}