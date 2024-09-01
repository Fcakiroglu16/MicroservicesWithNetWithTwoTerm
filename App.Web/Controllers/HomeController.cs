using App.Web.Models;
using App.Web.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.Web.Controllers
{
    public class HomeController(ILogger<HomeController> logger, MicroserviceOneService microserviceOneService)
        : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;


        public async Task<IActionResult> ClientCredentialRequest()
        {
            var exchange = await microserviceOneService.GetExchange();


            return View();
        }

        public IActionResult Index()
        {
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