using AssetManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ClientAssetManagement.Controllers
{
    //[Authorize(Roles = "Employee")]
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FormRequestEmployee()
        {
            return View();
        }
        public List<Item> GetItem()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = client.GetAsync("https://localhost:44393/api/Item").Result;
            var item = result.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Item>>(item);
            return data;

        }
    }

}
