using Northwind.Core.Entities;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Entities.DTOs
{
    public class OrderDetailDto : IDto
    {
        public int OrderId { get; set; }
        public string? CustomerID { get; set; }
        public int EmployeeID { get; set; }
        public string CustomerCompany { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerContactName { get; set; }
        public string EmployeeName { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public decimal Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string? ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
    }
}
