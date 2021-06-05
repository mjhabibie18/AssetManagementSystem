using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAssetManagement.Controllers
{
    public class ManagerController : Controller
    {
        public IActionResult DashManager()
        {
            return View();
        }

        public IActionResult DataTableManager()
        {
            return View();
        }

        public IActionResult ProcurementManager()
        {
            return View();
        }
    }
}
