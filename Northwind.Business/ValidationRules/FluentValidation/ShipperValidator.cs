using FluentValidation;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class ShipperValidator : AbstractValidator<Shipper>
    {
        public ShipperValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().MaximumLength(40);
            RuleFor(x => x.Phone).NotEmpty().MaximumLength(24);
        }
    }
}
