using Northwind.Business.Abstract;
using Northwind.Business.Constants;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.Core.Aspects.Autofac.Validation;
using Northwind.Core.Utilities.Results;
using Northwind.Core.Utilities.Results.Abstract;
using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concrete;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;
        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }
        [ValidationAspect(typeof(OrderValidator))]
        public IResult Add(Order order)
        {
            _orderDal.Add(order);
            return new SuccessResult(Messages.OrderAdded);
        }

        public IResult Delete(Order order)
        {
            _orderDal.Delete(order);
            return new SuccessResult(Messages.OrderDeleted);
        }

        public IDataResult<List<Order>> GetAll()
        {
            var data = _orderDal.GetAll();
            if (data != null)
            {
                return new SuccessDataResult<List<Order>>(data);
            }
            return new ErrorDataResult<List<Order>>();
        }

        public IDataResult<List<Order>> GetAllByCustomer(int customerId)
        {
            var data = _orderDal.GetAll(x => int.Parse(x.CustomerID) == customerId);
            if (data != null)
            {
                return new SuccessDataResult<List<Order>>(data);
            }
            return new ErrorDataResult<List<Order>>(data);
        }

        public IDataResult<List<Order>> GetAllByEmployee(int employeeId)
        {
            var data = _orderDal.GetAll(x => x.EmployeeID == employeeId);
            if (data != null)
            {
                return new SuccessDataResult<List<Order>>(data);
            }
            return new ErrorDataResult<List<Order>>(data);
        }

        public IDataResult<List<OrderDetailDto>> GetAllOrderDetails()
        {
            var data = _orderDal.GetOrderDetails();
            return new SuccessDataResult<List<OrderDetailDto>>(data);
        }

        public IDataResult<Order> GetById(int orderId)
        {
            var data = _orderDal.Get(x => x.OrderId == orderId);
            return new SuccessDataResult<Order>(data);
        }

        [ValidationAspect(typeof(OrderValidator))]
        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult(Messages.OrderUpdated);
        }
    }
}
