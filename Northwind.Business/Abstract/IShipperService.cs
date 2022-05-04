using Northwind.Core.Utilities.Results.Abstract;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Abstract
{
    public interface IShipperService
    {
        IDataResult<List<Shipper>> GetAll();
        IDataResult<Shipper> GetById(int shipperId);
        IResult Add(Shipper shipper);
        IResult Update(Shipper shipper);
        IResult Delete(Shipper shipper);
    }
}
