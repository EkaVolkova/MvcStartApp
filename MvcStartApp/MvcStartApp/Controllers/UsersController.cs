using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcStartApp.Models;
using MvcStartApp.Models.Db;
using MvcStartApp.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcStartApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IBlogRepository _repo;
        private readonly ILogger<HomeController> _logger;

        public UsersController(ILogger<HomeController> logger, IBlogRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _repo.GetUsers();

            Console.WriteLine("See all blog authors:");
            foreach (var author in authors)
                Console.WriteLine($"Author with id {author.Id}, named {author.FirstName}, joined {author.JoinDate}");


            return View(authors);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {

            return View();
        }

        [HttpPost]
        public async Task<string> Register(User user)
        {
            await _repo.AddUser(user);
            return "Suссes register";
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
