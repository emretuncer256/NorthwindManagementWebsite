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
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategory(int categoryId);
        IDataResult<List<ProductDetailDto>> GetAllProductDetails();
        IDataResult<List<ProductDetailDto>> GetAllProductDetailsByCategory(int categoryId);
        IDataResult<ProductDetailDto> GetProductDetail(int productId);
        IDataResult<int> GetProductCountByCategory(int categoryId);
        IDataResult<Product> GetById(int productId);
        IResult Add(Product product);
        IResult Update(Product product);
    }
}
