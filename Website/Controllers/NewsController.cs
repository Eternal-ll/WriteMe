﻿using Microsoft.AspNetCore.Mvc;
using Website.Infrastructure.Services.Interfaces;
using Website.ViewModels;

namespace Website.Controllers
{
    public class NewsController : Controller
    {
        private readonly IPostingService PostingService;

        public NewsController(IPostingService postingService)
        {
            PostingService = postingService;
        }

        [Route("/news")]
        public IActionResult News(NewsViewModel newsViewModel)
        {
            newsViewModel.NewsPosts = PostingService.GetSystemPosts();
            return View(newsViewModel);
        }

        public IActionResult GetPostsWithFilter(string text)
        {
            return View("PostsView", PostingService.GetSystemPostsWithFilter(text));
        }
    }
}
