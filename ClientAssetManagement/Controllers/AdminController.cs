using AssetManagement.Models;
using AssetManagement.ViewModels;
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
    public class AdminController : Controller
    {
        HttpClientHandler clientHandler = new HttpClientHandler(); 

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DataTableAdmin()
        {
            return View();
        }

        public IActionResult Procurement()
        {
            return View();
        }
        public string ProcurementApi(AddProcurement procurement)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(procurement), Encoding.UTF8, "application/json");
            var result = client.PostAsync("https://localhost:44393/api/Procurement/Insert-Procurement", stringContent).Result;
            if (result.IsSuccessStatusCode)
            {
                //return Ok(new { result });
                return Url.Action("Procurement", "Admin");
            }
            else
            {
                //return BadRequest(new { result });
                return "Error";
            }
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

        public List<object> GetDataTransaction()
        {
            var data = new List<object>();
            var token = HttpContext.Session.GetString("JWToken");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = client.GetAsync("https://localhost:44393/api/TransactionItem/GetDataTransaction").Result;
            
            if (result.IsSuccessStatusCode)
            {
                var trans = result.Content.ReadAsStringAsync().Result;
                var tra = JsonConvert.DeserializeObject<List<dynamic>>(trans);

                for(int i = 0; i < tra.Count; i++)
                {
                    data.Add(new
                    {
                        transId = tra[i].TransId.Value,
                        itemName = tra[i].Item.Value,
                        status = tra[i].Status.Value,
                        requestDate = tra[i].Request.Value,
                        returnDate = tra[i].Return.Value
                    });
                }
                return data;
            }
            return null;
        }

        [HttpGet]
        public Transactions Edit(int Id)
        {
            var token = HttpContext.Session.GetString("JWToken");
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = client.GetAsync("https://localhost:44393/api/Transaction/" + Id).Result;
            var apiResponse = response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<Transactions>(apiResponse.Result);
            return data;
        }

        [HttpPost]
        public HttpStatusCode Update([FromBody]Transactions transactions)
        {
            var trans = Edit(transactions.Id);
            trans.Status = transactions.Status;

            var token = HttpContext.Session.GetString("JWToken");
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            StringContent content = new StringContent(JsonConvert.SerializeObject(trans), Encoding.UTF8, "application/json");
            var result = httpClient.PutAsync("https://localhost:44393/api/Transaction/", content).Result;
            return result.StatusCode;
        }

        public List<object> GetStatus()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var data = new List<object>();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = client.GetAsync("https://localhost:44393/api/Transaction/GetStatus").Result;
            if (result.IsSuccessStatusCode)
            {
                var reports = result.Content.ReadAsStringAsync().Result;
                var rep = JsonConvert.DeserializeObject<List<dynamic>>(reports);

                for (int i = 0; i < rep.Count; i++)
                {
                    data.Add(new
                    {
                        status = rep[i].Status.Value,
                        total = rep[i].Total.Value
                    });
                }

                return data;
            }
            return null;
        }

        public List<object> GetItem()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var data = new List<object>();
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = client.GetAsync("https://localhost:44393/api/Transaction/GetItem").Result;
            if (result.IsSuccessStatusCode)
            {
                var reports = result.Content.ReadAsStringAsync().Result;
                var rep = JsonConvert.DeserializeObject<List<dynamic>>(reports);

                for (int i = 0; i < rep.Count; i++)
                {
                    data.Add(new
                    {
                        itemName = rep[i].Item.Value,
                        total = rep[i].Total.Value
                    });
                }

                return data;
            }
            return null;
        }
    }
}
