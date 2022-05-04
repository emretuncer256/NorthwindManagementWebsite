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
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Add(Category category)
        {
            IResult result = BusinessRules.Run(
                CheckIfCategoryNameExists(category.CategoryName)
            );
            if (result != null)
            {
                return result;
            }
            _categoryDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());
        }

        public IDataResult<Category> GetById(int categoryId)
        {
            var data = _categoryDal.Get(x => x.CategoryId == categoryId);
            if (data != null)
            {
                return new SuccessDataResult<Category>(data);
            }
            return new ErrorDataResult<Category>();
        }
        [ValidationAspect(typeof(CategoryValidator))]
        public IResult Update(Category category)
        {
            _categoryDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
        private IResult CheckIfCategoryNameExists(string categoryName)
        {
            var result = _categoryDal.GetAll(x => x.CategoryName.ToLower() == categoryName.ToLower()).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
