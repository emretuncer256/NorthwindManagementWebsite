using FluentValidation;
using Northwind.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.ValidationRules.FluentValidation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(x => x.OrderDate).NotEmpty();
            RuleFor(x => x.RequiredDate).NotEmpty();
            RuleFor(x => x.Freight).NotEmpty();
            RuleFor(x => x.ShipName).NotEmpty().MaximumLength(40);
            RuleFor(x => x.ShipAddress).NotEmpty().MaximumLength(60);
            RuleFor(x => x.ShipCity).NotEmpty();
            RuleFor(x => x.ShipPostalCode).NotEmpty();
            RuleFor(x => x.ShipCountry).NotEmpty();
        }
    }
}
