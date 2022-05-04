using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Entities.Concrete;
using Northwind.MVC.UI.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        ICategoryService _categoryService;
        IProductService _productService;

        public CategoriesController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View(_categoryService.GetAll().Data);
        }
        public IActionResult Products(int id)
        {
            var data = _productService.GetAllProductDetailsByCategory(id);
            ViewBag.currentCategoryName = _categoryService.GetById(id).Data.CategoryName;
            return View(data.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(Category category)
        {
            var result = _categoryService.Add(category);
            if (result.Success)
            {
                TempData["categoryAddSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var data = _categoryService.GetById(id).Data;
            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var result = _categoryService.Update(category);
            if (result.Success)
            {
                TempData["categoryUpdateSuccessMessage"] = result.Message;
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Export()
        {
            List<CategoryReportModel> data = (from c in _categoryService.GetAll().Data
                                              select new CategoryReportModel
                                              {
                                                  CategoryId = c.CategoryId,
                                                  CategoryName = c.CategoryName,
                                                  Description = c.Description,
                                                  ProductAmount = _productService.GetProductCountByCategory(c.CategoryId).Data
                                              }).ToList();
            return View(data);
        }
    }
}
