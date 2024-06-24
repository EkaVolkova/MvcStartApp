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
    public class LogesController : Controller
    {
        private readonly IRequestRepository _repo;
        private readonly ILogger<LogesController> _logger;

        public LogesController(ILogger<LogesController> logger, IRequestRepository repo)
        {
            _repo = repo;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var requests = await _repo.GetRequests();


            return View(requests);
        }
    }
}
