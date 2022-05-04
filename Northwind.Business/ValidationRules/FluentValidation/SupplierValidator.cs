using FluentValidation;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class SupplierValidator : AbstractValidator<Supplier>
    {
        public SupplierValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(40);
            RuleFor(x => x.ContactName).NotEmpty().MaximumLength(30);
            RuleFor(x => x.ContactTitle).MaximumLength(30);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(60);
            RuleFor(x => x.City).NotEmpty().MaximumLength(15);
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(24);
        }
    }
}
