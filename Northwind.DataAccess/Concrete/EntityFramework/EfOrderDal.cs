using Northwind.Core.DataAccess.EntityFramework;
using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concrete;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, NorthwindContext>, IOrderDal
    {
        public List<OrderDetailDto> GetOrderDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var data = from o in context.Orders
                           join e in context.Employees
                           on o.EmployeeID equals e.EmployeeId
                           join c in context.Customers
                           on o.CustomerID equals c.CustomerId
                           select new OrderDetailDto
                           {
                               OrderId = o.OrderId,
                               CustomerID = c.CustomerId,
                               EmployeeID = e.EmployeeId,
                               CustomerCompany = c.CompanyName,
                               CustomerContactName = c.ContactName,
                               CustomerPhone = c.Phone,
                               EmployeeName = e.FirstName + " " + e.LastName,
                               Freight = o.Freight,
                               OrderDate = o.OrderDate,
                               RequiredDate = o.RequiredDate,
                               ShipAddress = o.ShipAddress,
                               ShipCity = o.ShipCity,
                               ShipCountry = o.ShipCountry,
                               ShipName = o.ShipName,
                               ShippedDate = o.ShippedDate,
                               ShipPostalCode = o.ShipPostalCode,
                               ShipRegion = o.ShipRegion
                           };
                return data.ToList();
            }
        }
    }
}
