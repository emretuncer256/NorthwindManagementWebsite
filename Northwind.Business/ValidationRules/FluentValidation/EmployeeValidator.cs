using FluentValidation;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
            RuleFor(x => x.FirstName).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Title).MaximumLength(30);
            RuleFor(x => x.BirthDate).NotEmpty();
            RuleFor(x => x.HireDate).NotEmpty();
            RuleFor(x => x.Address).NotEmpty().MaximumLength(60);
            RuleFor(x => x.City).NotEmpty().MaximumLength(15);
            RuleFor(x => x.PostalCode).NotEmpty().MaximumLength(10);
            RuleFor(x => x.Country).NotEmpty().MaximumLength(15);
        }
    }
}
