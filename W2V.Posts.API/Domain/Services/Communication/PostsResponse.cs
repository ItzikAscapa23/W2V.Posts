using System.Collections.Generic;
using W2V.Posts.API.Domain.Models;

namespace W2V.Posts.API.Domain.Services.Communication
{
    public class PostsResponse : BaseResponse
    {
        public IEnumerable<Post> Posts { get; }
        public PostsResponse(bool success, string message, IEnumerable<Post> posts) : base(success, message)
        {
            Posts = posts;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="posts">Handle posts.</param>
        /// <returns>Response.</returns>
        public PostsResponse(IEnumerable<Post> posts) : this(true, null, posts)
        { }

        /// <summary>
        /// Creates an error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public PostsResponse(string message) : this(false, message, null)
        { }

        public PostsResponse(bool success, string message = null) : this(success, message, null)
        { }
    }
}
