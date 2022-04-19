using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TableBookingSystem.Web.Authorization.Attributes;
using TableBookingSystem.Web.Controllers.Common;
using TableBookingSystem.Web.Models;

namespace TableBookingSystem.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AdminRole]
        public IActionResult Index()
        {
            return View();
        }
        [Route("/")]
        [HttpGet("/Welcome")]
        public IActionResult Welcome()
        {
            return View("WelcomePage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
