using Northwind.Business.Abstract;
using Northwind.Business.Constants;
using Northwind.Business.ValidationRules.FluentValidation;
using Northwind.Core.CrossCuttingConcerns.Validation;
using Northwind.Core.Utilities.Results;
using Northwind.Core.Utilities.Results.Abstract;
using Northwind.DataAccess.Abstract;
using Northwind.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Northwind.Core.Aspects.Autofac.Validation;
using Northwind.Core.Utilities.Business;
using Northwind.Entities.DTOs;

namespace Northwind.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(
                CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId)
            );
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        public IDataResult<List<Product>> GetAllByCategory(int categoryId)
        {
            var data = _productDal.GetAll(x => x.CategoryId == categoryId);
            return new SuccessDataResult<List<Product>>(data);
        }

        public IDataResult<Product> GetById(int productId)
        {
            var data = _productDal.Get(x => x.ProductId == productId);
            if (data != null)
            {
                return new SuccessDataResult<Product>(data);
            }
            return new ErrorDataResult<Product>();

        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(
                CheckIfProductCountOfCategoryCorrect(product.CategoryId)
            );
            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _productDal.GetAll(x => x.CategoryId == categoryId);
            if (result.Count >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        public IDataResult<List<ProductDetailDto>> GetAllProductDetails()
        {
            var data = _productDal.GetProductDetails();
            return new SuccessDataResult<List<ProductDetailDto>>(data);
        }

        public IDataResult<ProductDetailDto> GetProductDetail(int productId)
        {
            var data = _productDal.GetProductDetail(x => x.ProductId == productId);
            return new SuccessDataResult<ProductDetailDto>(data);
        }

        public IDataResult<List<ProductDetailDto>> GetAllProductDetailsByCategory(int categoryId)
        {
            var data = _productDal.GetProductDetails(x => x.CategoryId == categoryId);
            return new SuccessDataResult<List<ProductDetailDto>>(data);
        }

        public IDataResult<int> GetProductCountByCategory(int categoryId)
        {
            var count = _productDal.GetAll(x => x.CategoryId == categoryId).Count;
            return new SuccessDataResult<int>(count);
        }
    }
}
