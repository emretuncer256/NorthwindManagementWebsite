using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.MVC.UI.Controllers
{
    [AllowAnonymous]
    public class ErrorPageController : Controller
    {
        public IActionResult Page404()
        {
            //Page Not Found
            return View();
        }
        public IActionResult Page500()
        {
            //Internal Server Error
            return View();
        }
        public IActionResult Page503()
        {
            //Server Unavailable
            return View();
        }
    }
}
