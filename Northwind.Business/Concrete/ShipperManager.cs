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
    public class ShipperManager : IShipperService
    {
        IShipperDal _shipperDal;
        public ShipperManager(IShipperDal shipperDal)
        {
            _shipperDal = shipperDal;
        }
        [ValidationAspect(typeof(ShipperValidator))]
        public IResult Add(Shipper shipper)
        {
            IResult result = BusinessRules.Run(
                CheckIfShipperCompanyNameExists(shipper.CompanyName)
            );
            if (result != null)
            {
                return result;
            }
            _shipperDal.Add(shipper);
            return new SuccessResult(Messages.ShipperAdded);
        }

        public IResult Delete(Shipper shipper)
        {
            _shipperDal.Delete(shipper);
            return new SuccessResult(Messages.ShipperDeleted);
        }

        public IDataResult<List<Shipper>> GetAll()
        {
            return new SuccessDataResult<List<Shipper>>(_shipperDal.GetAll());
        }

        public IDataResult<Shipper> GetById(int shipperId)
        {
            var data = _shipperDal.Get(x => x.ShipperID == shipperId);
            if (data != null)
            {
                return new SuccessDataResult<Shipper>(data);
            }
            return new ErrorDataResult<Shipper>();
        }

        [ValidationAspect(typeof(ShipperValidator))]
        public IResult Update(Shipper shipper)
        {
            _shipperDal.Update(shipper);
            return new SuccessResult(Messages.ShipperUpdated);
        }

        private IResult CheckIfShipperCompanyNameExists(string companyName)
        {
            var result = _shipperDal.GetAll(x => x.CompanyName.ToLower() == companyName.ToLower()).Any();
            if (result)
            {
                return new ErrorResult(Messages.ShipperCompanyNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
