using Northwind.Core.Utilities.Results.Abstract;
using Northwind.Entities.Concrete;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<Order>> GetAll();
        IDataResult<List<Order>> GetAllByCustomer(int customerId);
        IDataResult<List<Order>> GetAllByEmployee(int employeeId);
        IDataResult<List<OrderDetailDto>> GetAllOrderDetails();
        IDataResult<Order> GetById(int orderId);
        IResult Add(Order order);
        IResult Update(Order order);
        IResult Delete(Order order);
    }
}
