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
    public class HomeController : Controller
    {
        private readonly IBlogRepository _repo;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IBlogRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }
        public async Task<IActionResult> Authors()
        {
            var authors = await _repo.GetUsers();

            Console.WriteLine("See all blog authors:");
            foreach (var author in authors)
                Console.WriteLine($"Author with id {author.Id}, named {author.FirstName}, joined {author.JoinDate}");


            return View(authors);
        }

        public async Task<IActionResult> Index()
        {
            var user = new User
            {
                Id = new Guid(),
                FirstName = "John",
                LastName = "Smith",
                JoinDate = DateTime.Now
            };

            await _repo.AddUser(user);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
