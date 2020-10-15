﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data;
using Blog.Data.FileManager;
using Blog.Data.Repository;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        public IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(IRepository repo, IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index(string category) => View(string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category));

        public IActionResult Post(int id) => View(_repo.GetPost(id));

        [HttpGet("/Image/{image}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult Image(string image) => new FileStreamResult(_fileManager.ImageStream(image), $"image/{image.Substring(image.LastIndexOf('.') + 1)}");

        // Old Code that works
        // public IActionResult Index(string category)
        // {
        //
        //     var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category);
        //     return View(posts);
        // }

        // public IActionResult Post(int id)
        // {
        //     var post = _repo.GetPost(id);
        //     return View(post);
        // }

        // [HttpGet("/Image/{image}")]
        // public IActionResult Image(string image)
        // {
        //     var mime = image.Substring(image.LastIndexOf('.') + 1);
        //     return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        // }
    }
}
