using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CachingMVC.Models;
using CachingMVC.Repositories;
using CachingMVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace CachingMVC.Controllers
{
    public class PostsController : Controller
    {
        PostRepository _postRepository;
        CashService _cashService;


        public PostsController(PostRepository postRepository, CashService cashService)
        {
            _postRepository = postRepository;
            _postRepository.Initialize();
            _cashService = cashService;
        }


        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {

            var post = await _cashService.TryExecuteAsync<Post>(() => _postRepository.GetPost(id), id);

            if (post != null)
                return Content($"Description: {post.Description}");
            return Content("Post not found");
        }
    }
}