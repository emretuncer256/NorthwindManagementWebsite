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
    public class SuppliersController : Controller
    {
        ISupplierService _supplierService;
        public SuppliersController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Index(int p = 1)
        {
            var data = _supplierService.GetAll().Data.ToPagedList(p, 9);
            return View(data);
        }
        public IActionResult View(int id)
        {
            var data = _supplierService.GetById(id).Data;
            return View(data);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _supplierService.GetById(id).Data;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            var result = _supplierService.Update(supplier);
            if (result.Success)
            {
                TempData["supplierUpdateSuccessMessage"] = result.Message;
                return RedirectToAction("View", new { id = supplier.SupplierID });
            }
            return View();
        }
        public IActionResult Delete(int id)
        {
            var data = _supplierService.GetById(id).Data;
            var result = _supplierService.Delete(data);
            if (result.Success)
            {
                TempData["supplierDeleteSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return View("Index");
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Supplier supplier)
        {
            var result = _supplierService.Add(supplier);
            if (result.Success)
            {
                TempData["supplierAddSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Export()
        {
            var data = _supplierService.GetAll().Data;
            return View(data);
        }
    }
}
