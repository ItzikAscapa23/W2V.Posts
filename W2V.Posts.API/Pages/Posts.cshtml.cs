using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using W2V.Posts.API.Domain.Models;
using W2V.Posts.API.Domain.Services;

namespace W2V.Posts.API.Pages
{
    public class PostsModel : PageModel
    {
        public IEnumerable<Post> PostList { get; private set; }
        private readonly IPostService _postService;

        public PostsModel(IPostService postService)
        {
            _postService = postService;
        }

        public async Task OnGet()
        {
            PostList = await _postService.GetAllPosts();
        }
    }
}