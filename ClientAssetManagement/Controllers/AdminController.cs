using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAssetManagement.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult DashAdmin()
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
    }
}
