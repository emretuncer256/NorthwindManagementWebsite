using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomersController : Controller
    {
        ICustomerService _customerService;
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var data = _customerService.GetAll().Data;
            return View(data);
        }
        public IActionResult View(string id)
        {
            var data = _customerService.GetById(id).Data;
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var data = _customerService.GetById(id).Data;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Customer customer)
        {
            var result = _customerService.Update(customer);
            if (result.Success)
            {
                TempData["customerSuccessUpdateMessage"] = result.Message;
                return RedirectToAction("View", new { id = customer.CustomerId });
            }
            return View();
        }
        public IActionResult Export()
        {
            var data = _customerService.GetAll().Data;
            return View(data);
        }
    }
}
