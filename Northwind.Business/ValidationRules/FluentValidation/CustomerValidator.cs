using FluentValidation;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MinimumLength(3).MaximumLength(40);
            RuleFor(x => x.ContactName).NotEmpty().MinimumLength(3).MaximumLength(30);
            RuleFor(x => x.Address).NotEmpty().MaximumLength(60);
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.PostalCode).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(24);
        }
    }
}
