using Northwind.Business.Abstract;
using Northwind.Business.Constants;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.Core.Aspects.Autofac.Validation;
using Northwind.Core.Utilities.Results;
using Northwind.Core.Utilities.Results.Abstract;
using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }
        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Add(Employee employee)
        {
            _employeeDal.Add(employee);
            return new SuccessResult(Messages.EmployeeAdded);
        }

        public IDataResult<List<Employee>> GetAll()
        {
            return new SuccessDataResult<List<Employee>>(_employeeDal.GetAll());
        }

        public IDataResult<Employee> GetById(int employeeId)
        {
            var data = _employeeDal.Get(x => x.EmployeeId == employeeId);
            if (data != null)
            {
                return new SuccessDataResult<Employee>(data);
            }
            return new ErrorDataResult<Employee>();
        }
        [ValidationAspect(typeof(EmployeeValidator))]
        public IResult Update(Employee employee)
        {
            _employeeDal.Update(employee);
            return new SuccessResult(Messages.EmployeeUpdated);
        }
    }
}
