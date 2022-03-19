using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.RedisCache.Cacheable.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Testing.Models;

namespace Testing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICacheable _cacheable;

        public HomeController(ILogger<HomeController> logger, ICacheable cacheable)
        {
            _logger = logger;
            _cacheable = cacheable;
        }

        public async Task<IActionResult> Index()
        {
            var companyId = Guid.Empty; // from js
            var userId = Guid.Empty; // loggedIn user

            var cacheKey = $"{companyId}_{userId}";

            async Task<string> BigData()
            {
                var t = companyId.ToString();

                await Task.Delay(1); // make a call to the api

                var employeeId = Guid.NewGuid();

                return "This is a complex operation"; // return object from api
            };

            var model = await _cacheable.RememberAsync(cacheKey, BigData);

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
