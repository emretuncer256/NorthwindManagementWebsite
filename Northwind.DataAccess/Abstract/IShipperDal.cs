using Northwind.Core.DataAccess;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Abstract
{
    public interface IShipperDal : IEntityRepository<Shipper>
    {
    }
}
