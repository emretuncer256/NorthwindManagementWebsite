using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Entities.Concrete;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Northwind.MVC.UI.Controllers
{
    [AllowAnonymous]
    public class UsersController : Controller
    {
        IAuthService _authService;
        public UsersController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginDto user)
        {
            var userToLogin = _authService.Login(user);
            if (userToLogin.Success)
            {
                TempData["loginSuccessMessage"] = userToLogin.Message;
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email)
                };
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    IsPersistent = user.RememberMe,
                };
                var roles = _authService.GetUserClaims(_authService.GetUserByEmail(user.Email).Data);
                if (roles.Data.Count > 0)
                {
                    foreach (var role in roles.Data)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                }
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal, properties);
                return RedirectToAction("", "Home");
            }
            TempData["loginErrorMessage"] = userToLogin.Message;
            return View(user);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(UserForRegisterDto user)
        {
            if (user.Password != user.RetypePassword)
            {
                TempData["registerErrorMessage"] = "Your password does not match the first";
                return View(user);
            }
            var userExists = _authService.UserEmailExists(user.Email);
            if (!userExists.Success)
            {
                TempData["registerErrorMessage"] = userExists.Message;
                return View(user);
            }

            var registerResult = _authService.Register(user, user.Password);
            if (registerResult.Success)
            {
                TempData["registerSuccessMessage"] = registerResult.Message;
                return RedirectToAction("Login");
            }
            TempData["registerErrorMessage"] = registerResult.Message;
            return View(user);
        }
        public async Task<IActionResult> Logout()
        {
            TempData["logoutSucessfull"] = "User successfully log out";
            await HttpContext.SignOutAsync();
            return RedirectToAction("", "Home");
        }
        public IActionResult ForgotMyPassword()
        {
            return View();
        }
    }
}
