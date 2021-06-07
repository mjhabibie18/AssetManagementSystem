using AssetManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientAssetManagement.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataTableManager()
        {
            return View();
        }

        public IActionResult Manager()
        {
            return View();
        }

        public List<Procurement> GetProcurement()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = client.GetAsync("https://localhost:44393/api/Procurement").Result;
            var procu = result.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Procurement>>(procu);
            return data;

        }

        public List<Transactions> GetTransaction()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = client.GetAsync("https://localhost:44393/api/Transaction").Result;
            var trans = result.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<List<Transactions>>(trans);
            return data;
        }

       [HttpPost]
       public string Approved(int Id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            StringContent content = new StringContent(JsonConvert.SerializeObject(Id), Encoding.UTF8, "application/json");
            var result = client.PostAsync("https://localhost:44393/api/Procurement/Approve-Procurement/" + Id, content).Result;
            return Url.Action("Manager", "Manager");
        }

        [HttpPut]
        public HttpStatusCode Update(Transactions model)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44393/api/Transaction/", content).Result;
            return result.StatusCode;
        }
    }
}
