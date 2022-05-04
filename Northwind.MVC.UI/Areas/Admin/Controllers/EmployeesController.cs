using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace Northwind.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeesController : Controller
    {
        IEmployeeService _employeeService;
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index(int p = 1)
        {
            IPagedList data = _employeeService.GetAll().Data.ToPagedList(p, 6);
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _employeeService.GetById(id).Data;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            var result = _employeeService.Update(employee);
            if (result.Success)
            {
                TempData["employeeUpdateSuccessMessage"] = result.Message;
                return RedirectToAction("View", new { id = employee.EmployeeId });
            }
            return View();
        }
        public IActionResult View(int id)
        {
            var data = _employeeService.GetById(id).Data;
            return View(data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            var result = _employeeService.Add(employee);
            if (result.Success)
            {
                TempData["employeeAddSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Export()
        {
            var data = _employeeService.GetAll().Data;
            return View(data);
        }
    }
}
