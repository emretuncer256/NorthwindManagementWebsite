using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Core.Entities.Concrete;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Northwind.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MyProfileController : Controller
    {
        IAuthService _authService;
        public MyProfileController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            var user = _authService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)).Data;
            ViewData["userClaims"] = _authService.GetUserClaims(user).Data;
            return View(user);
        }
        [HttpGet]
        public IActionResult Edit()
        {
            var user = _authService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)).Data;
            var userDto = new UserForUpdateDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            return View(userDto);
        }
        [HttpPost]
        public IActionResult Edit(UserForUpdateDto userForUpdate)
        {

            if (userForUpdate.Password == userForUpdate.RetypePassword)
            {
                var user = _authService.GetUserByEmail(User.FindFirstValue(ClaimTypes.Email)).Data;
                userForUpdate.Id = user.Id;
                userForUpdate.Email = user.Email;
                var result = _authService.UpdateUser(userForUpdate, userForUpdate.Password);
                if (result.Success)
                {
                    TempData["userSuccessfullUpdate"] = result.Message;
                    return RedirectToAction("", "MyProfile");
                }
                TempData["userErrorUpdate"] = result.Message;
                return View(userForUpdate);
            }
            TempData["userPasswordMatchError"] = "Your password does not match the first";
            return View(userForUpdate);
        }
    }
}
