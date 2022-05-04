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
    public class ShippersController : Controller
    {
        IShipperService _shipperService;
        public ShippersController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        public IActionResult Index()
        {
            var data = _shipperService.GetAll().Data;
            return View(data);
        }

        [HttpPost]
        public IActionResult Add(Shipper shipper)
        {
            var result = _shipperService.Add(shipper);
            if (result.Success)
            {
                TempData["shipperAddSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return PartialView();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _shipperService.GetById(id).Data;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Shipper shipper)
        {
            var result = _shipperService.Update(shipper);
            if (result.Success)
            {
                TempData["shipperUpdateSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Export()
        {
            var data = _shipperService.GetAll().Data;
            return View(data);
        }
        public IActionResult Delete(int id)
        {
            var data = _shipperService.GetById(id).Data;
            var result = _shipperService.Delete(data);
            if (result.Success)
            {
                TempData["shipperDeleteSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return View("Index");
        }
    }
}
