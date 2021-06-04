using ClientAssetManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
namespace ClientAssetManagement.Controllers

{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var token = HttpContext.Session.GetString("JWToken");


            //if (token != null)
            //{
            //    var jwtReader = new JwtSecurityTokenHandler();
            //    var jwt = jwtReader.ReadJwtToken(token);

            //    var email = jwt.Claims.First(c => c.Type == "unique_name").Value;
            //    ViewData["token"] = email;
            //    return View();
            //}
            //return Unauthorized();
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