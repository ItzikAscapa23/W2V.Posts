using System.Collections.Generic;
using System.Threading.Tasks;
using W2V.Posts.API.Domain.Models;

namespace W2V.Posts.API.Domain.Repositories
{
    public interface IPostsRepository
    {
        Task<IEnumerable<Post>> GetTopPosts();
        Task CreatePost(Post p);
        Task IncrementUpVotes(long postId);
        Task IncrementDownVotes(long postId);
        Task DeletePost(long postId);
        Task<Post> GetPostById(long postId);
    }
}
