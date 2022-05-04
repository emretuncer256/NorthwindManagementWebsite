using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Northwind.Business.Abstract;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrdersController : Controller
    {
        IOrderService _orderService;
        ICustomerService _customerService;
        IEmployeeService _employeeService;
        public OrdersController(IOrderService orderService, ICustomerService customerService, IEmployeeService employeeService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var data = _orderService.GetAll().Data;
            return View(data);
        }
        public IActionResult View(int id)
        {
            var data = _orderService.GetById(id).Data;
            ViewBag.OrderCustomer = _customerService.GetById(data.CustomerID).Data;
            ViewBag.OrderEmployee = _employeeService.GetById(data.EmployeeID).Data;
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<SelectListItem> employees = (from e in _employeeService.GetAll().Data
                                              select new SelectListItem
                                              {
                                                  Text = e.FirstName + " " + e.LastName,
                                                  Value = e.EmployeeId.ToString()
                                              }).ToList();
            List<SelectListItem> customers = (from c in _customerService.GetAll().Data
                                              select new SelectListItem
                                              {
                                                  Text = c.CompanyName,
                                                  Value = c.CustomerId
                                              }).ToList();
            ViewBag.OrderEmployees = employees;
            ViewBag.OrderCustomers = customers;
            var data = _orderService.GetById(id).Data;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Order order)
        {
            var result = _orderService.Update(order);
            if (result.Success)
            {
                TempData["orderUpdateSuccessMessage"] = result.Message;
                return RedirectToAction("View", new { id = order.OrderId });
            }
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            List<SelectListItem> employees = (from e in _employeeService.GetAll().Data
                                              select new SelectListItem
                                              {
                                                  Text = e.FirstName + " " + e.LastName,
                                                  Value = e.EmployeeId.ToString()
                                              }).ToList();
            List<SelectListItem> customers = (from c in _customerService.GetAll().Data
                                              select new SelectListItem
                                              {
                                                  Text = c.CompanyName,
                                                  Value = c.CustomerId
                                              }).ToList();
            ViewBag.OrderEmployees = employees;
            ViewBag.OrderCustomers = customers;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Order order)
        {
            var result = _orderService.Add(order);
            if (result.Success)
            {
                TempData["orderAddSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Export()
        {
            var data = _orderService.GetAllOrderDetails().Data;
            return View(data);
        }
    }
}
