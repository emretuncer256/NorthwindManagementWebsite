using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Northwind.Business.Abstract;
using Northwind.Entities.Concrete;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.MVC.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        IProductService _productService;
        ICustomerService _customerService;
        IEmployeeService _employeeService;
        IOrderService _orderService;

        public HomeController(IProductService productService, ICustomerService customerService, IEmployeeService employeeService, IOrderService orderService)
        {
            _productService = productService;
            _customerService = customerService;
            _employeeService = employeeService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            List<OrderDetailDto> orderDetails = _orderService.GetAllOrderDetails().Data;
            List<ProductDetailDto> productDetails = _productService.GetAllProductDetails().Data;
            ViewData["CustomersForCard"] = _customerService.GetAll().Data;
            ViewData["ProductsForCard"] = _productService.GetAll().Data;
            ViewData["EmployeesForCard"] = _employeeService.GetAll().Data;
            ViewData["OrdersForCard"] = _orderService.GetAll().Data;
            ViewData["LatestOrders"] = Enumerable.Reverse(orderDetails.OrderBy(x => x.OrderDate).TakeLast(7)).ToList();
            ViewData["LatesProducts"] = Enumerable.Reverse(productDetails.OrderBy(x => x.ProductId).TakeLast(5)).ToList();
            return View();
        }
    }
}
