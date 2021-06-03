using AssetManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientAssetManagement.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            //HttpContext.Session.Remove("JWToken");
            return View();
        }
        public IActionResult ForgetPassword()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            if (Request.Query.ContainsKey("token"))
            {
                var token = Request.Query["token"].ToString();
                ViewData["token"] = token;
                return View();
            }
            return NotFound();
        }
        public IActionResult ForgetPasswordApi(ForgetPassword forgot)
        {
            var client = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(forgot), Encoding.UTF8, "application/json");
            var result = client.PostAsync("https://localhost:44393/api/Account/ForgetPassword", stringContent).Result;
            if (result.IsSuccessStatusCode)
            {
                return Ok(new { result });
            }
            else
            {
                return BadRequest(new { result });
            }
        }

        public IActionResult ResetPasswordApi(ResetPassword reset)
        {
            var client = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(reset), Encoding.UTF8, "application/json");
            var result = client.PutAsync("https://localhost:44393/api/Account/ResetPassword", stringContent).Result;
            if (result.IsSuccessStatusCode)
            {
                return Ok(new { result });
                //return Url.Action("Index", "Authentication");
            }
            else
            {
                return BadRequest(new { result });
            }
        }

        public string LoginApi(Login login)
        {
            var client = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = client.PostAsync("https://localhost:44393/api/Account/Login", stringContent).Result;
            var token = result.Content.ReadAsStringAsync().Result;

            HttpContext.Session.SetString("JWToken", token);

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (result.IsSuccessStatusCode)
            {
                //return RedirectToRoute(new { action = "Index", controller = "Home", area = "" });
                //return Ok(new { result });
                return Url.Action("Index", "Home");
            }
            else
            {
                return "Error";
                //return BadRequest(new { result });
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("JWToken");
            return RedirectToAction("Index", "Authentication");
        }
        public string RegisterApi(Register register)
        {
            var client = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            var result = client.PostAsync("https://localhost:44393/api/Account/Register", stringContent).Result;
            if (result.IsSuccessStatusCode)
            {
                //return Ok(new { result });
                return Url.Action("Index", "Authentication");
            }
            else
            {
                //return BadRequest(new { result });
                return "Error";
            }
        }

    }
}
