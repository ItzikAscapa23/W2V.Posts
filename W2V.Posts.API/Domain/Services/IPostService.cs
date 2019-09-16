using System.Collections.Generic;
using System.Threading.Tasks;
using W2V.Posts.API.Domain.Models;
using W2V.Posts.API.Domain.Services.Communication;

namespace W2V.Posts.API.Domain.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPosts();
        Task<PostResponse> CreatePost(Post post);
        Task<PostResponse> IncrementUpVotes(long postId);
        Task<PostResponse> IncrementDownVotes(long postId);
        Task<PostResponse> DeletePost(long postId);
    }
}