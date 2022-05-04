using Northwind.Business.Abstract;
using Northwind.Business.Constants;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.Core.Aspects.Autofac.Validation;
using Northwind.Core.Utilities.Business;
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
    public class SupplierManager : ISupplierService
    {
        ISupplierDal _supplierDal;

        public SupplierManager(ISupplierDal supplierDal)
        {
            _supplierDal = supplierDal;
        }

        [ValidationAspect(typeof(SupplierValidator))]
        public IResult Add(Supplier supplier)
        {
            IResult result = BusinessRules.Run(
                CheckIfSupplierCompanyNameExists(supplier.CompanyName)
            );
            if (result != null)
            {
                return result;
            }
            _supplierDal.Add(supplier);
            return new SuccessResult(Messages.SupplierAdded);
        }

        public IResult Delete(Supplier supplier)
        {
            _supplierDal.Delete(supplier);
            return new SuccessResult(Messages.SupplierDeleted);
        }

        public IDataResult<List<Supplier>> GetAll()
        {
            return new SuccessDataResult<List<Supplier>>(_supplierDal.GetAll());
        }

        public IDataResult<Supplier> GetById(int supplierId)
        {
            var data = _supplierDal.Get(x => x.SupplierID == supplierId);
            if (data != null)
            {
                return new SuccessDataResult<Supplier>(data);
            }
            return new ErrorDataResult<Supplier>();
        }

        [ValidationAspect(typeof(SupplierValidator))]
        public IResult Update(Supplier supplier)
        {
            _supplierDal.Update(supplier);
            return new SuccessResult(Messages.SupplierUpdated);
        }

        private IResult CheckIfSupplierCompanyNameExists(string companyName)
        {
            var result = _supplierDal.GetAll(x => x.CompanyName.ToLower() == companyName.ToLower()).Any();
            if (result)
            {
                return new ErrorResult(Messages.SupplierCompanyNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
