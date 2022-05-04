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
    public class ProductsController : Controller
    {
        IProductService _productService;
        ICategoryService _categoryService;
        ISupplierService _supplierService;

        public ProductsController(IProductService productService, ICategoryService categoryService, ISupplierService supplierService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _supplierService = supplierService;
        }

        public IActionResult Index()
        {
            var data = _productService.GetAll().Data;
            return View(data);
        }

        public IActionResult View(int id)
        {
            var data = _productService.GetProductDetail(id).Data;
            return View(data);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<SelectListItem> categories = (from c in _categoryService.GetAll().Data
                                               select new SelectListItem
                                               {
                                                   Text = c.CategoryName,
                                                   Value = c.CategoryId.ToString()
                                               }).ToList();
            List<SelectListItem> suppliers = (from s in _supplierService.GetAll().Data
                                              select new SelectListItem
                                              {
                                                  Text = s.CompanyName,
                                                  Value = s.SupplierID.ToString()
                                              }).ToList();
            ViewBag.Categories = categories;
            ViewBag.Suppliers = suppliers;
            var data = _productService.GetById(id).Data;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            var result = _productService.Update(product);
            if (result.Success)
            {
                TempData["productUpdateSuccessMessage"] = result.Message;
                return RedirectToAction("View", "Products", new { area = "Admin", id = product.ProductId });
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Add()
        {
            List<SelectListItem> categories = (from c in _categoryService.GetAll().Data
                                               select new SelectListItem
                                               {
                                                   Text = c.CategoryName,
                                                   Value = c.CategoryId.ToString()
                                               }).ToList();
            List<SelectListItem> suppliers = (from s in _supplierService.GetAll().Data
                                              select new SelectListItem
                                              {
                                                  Text = s.CompanyName,
                                                  Value = s.SupplierID.ToString()
                                              }).ToList();
            ViewBag.Categories = categories;
            ViewBag.Suppliers = suppliers;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                TempData["productAddSuccessMessage"] = result.Message;
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }
            return View();
        }
        public IActionResult Export()
        {
            var data = _productService.GetAllProductDetails().Data;
            return View(data);
        }
    }
}
