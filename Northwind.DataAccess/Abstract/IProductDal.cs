using Northwind.Core.DataAccess;
using Northwind.Entities.Concrete;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter = null);
        ProductDetailDto GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter);
    }
}
