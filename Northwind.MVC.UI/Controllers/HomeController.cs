using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind.Business.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Northwind.MVC.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        IAuthService _authService;
        public HomeController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            if (User.FindFirstValue(ClaimTypes.Email) != null)
            {
                var user = _authService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)).Data;
                ViewBag.User = user;
            }
            return View();
        }
    }
}
