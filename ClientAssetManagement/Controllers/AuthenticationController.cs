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
            return View();
        }
        
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        
        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        


        public IActionResult RegisterApi(Register register)
        {
            var userClient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(register), Encoding.UTF8, "application/json");
            var result = userClient.PostAsync("https://localhost:44393/api/Account/Register", stringContent).Result;
            if (result.IsSuccessStatusCode)
            {
                return Ok(new { result });
                //return Url.Action("Index", "Authentication");
            }
            else
            {
                return BadRequest(new { result });
                //return "Error";
            }
        }

        public IActionResult LoginApi(Login login)
        {
            var userClient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = userClient.PostAsync("https://localhost:44380/api/accounts/login", stringContent).Result;
            var token = result.Content.ReadAsStringAsync().Result;

            HttpContext.Session.SetString("JWToken", token);

            userClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (result.IsSuccessStatusCode)
            {
                //return RedirectToRoute(new { action = "Index", controller = "Home", area = "" });
                return Ok(new { result });
                //return Url.Action("Index", "Home");
            }
            else
            {
                //return "Error";
                return BadRequest(new { result });
            }
        }

        public IActionResult ForgetPasswordApi(ForgetPassword forget)
        {
            var userClient = new HttpClient();
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(forget), Encoding.UTF8, "application/json");
            var result = userClient.PostAsync("https://localhost:44393/api/Account/ForgetPassword", stringContent).Result;
            if (result.IsSuccessStatusCode)
            {
                return Ok(new { result });
            }
            else
            {
                return BadRequest(new { result });
            }
        }
    }
}
