using Northwind.Core.DataAccess.EntityFramework;
using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concrete;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public ProductDetailDto GetProductDetail(Expression<Func<ProductDetailDto, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var data = from p in context.Products
                           join c in context.Categories
                           on p.CategoryId equals c.CategoryId
                           select new ProductDetailDto
                           {
                               ProductId = p.ProductId,
                               CategoryId = c.CategoryId,
                               ProductName = p.ProductName,
                               CategoryName = c.CategoryName,
                               UnitPrice = p.UnitPrice,
                               UnitsInStock = p.UnitsInStock,
                               QuantityPerUnit = p.QuantityPerUnit
                           };
                return data.SingleOrDefault(filter);
            }
        }

        public List<ProductDetailDto> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var data = from p in context.Products
                           join c in context.Categories
                           on p.CategoryId equals c.CategoryId
                           select new ProductDetailDto
                           {
                               ProductId = p.ProductId,
                               CategoryId = c.CategoryId,
                               ProductName = p.ProductName,
                               CategoryName = c.CategoryName,
                               UnitPrice = p.UnitPrice,
                               UnitsInStock = p.UnitsInStock,
                               QuantityPerUnit = p.QuantityPerUnit
                           };
                return filter == null ? data.ToList() : data.Where(filter).ToList();
            }
        }
    }
}
