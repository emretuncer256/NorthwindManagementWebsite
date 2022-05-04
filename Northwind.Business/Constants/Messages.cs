using Northwind.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Constants
{
    public static class Messages
    {
        public const string CategoryAdded = "The Category successfully added";
        public const string CategoryUpdated = "The Category successfully updated";
        public const string CategoryLimitExceeded = "Category limit is exceeded";
        public const string CategoryNameAlreadyExists = "The Category name is already exist";

        public const string CustomerAdded = "The Customer successfully added";
        public const string CustomerUpdated = "The Customer successfully updated";

        public const string EmployeeAdded = "The Employee successfully added";
        public const string EmployeeUpdated = "The Employee successfully updated";

        public const string OrderAdded = "The Order successfully added";
        public const string OrderUpdated = "The Order successfully updated";
        public const string OrderDeleted = "The Order successfully deleted";

        public const string ProductAdded = "The Product successfully added";
        public const string ProductUpdated = "The Product successfully updated";
        public const string ProductDeleted = "The Product successfully deleted";
        public const string ProductNameAlreadyExists = "The Product name is already exist";
        public const string ProductCountOfCategoryError = "A category can have up to 15 products";

        public const string ShipperAdded = "The Shipper successfully added";
        public const string ShipperUpdated = "The Shipper successfully updated";
        public const string ShipperCompanyNameAlreadyExists = "The Shipper company name is already exist";
        public const string ShipperDeleted = "Shipper successfully deleted";

        public const string SupplierAdded = "The Supplier successfully added";
        public const string SupplierUpdated = "The Supplier successfully updated";
        public const string SupplierDeleted = "User successfully deleted";
        public const string SupplierCompanyNameAlreadyExists = "The Supplier company name is already exist";

        public const string UserAdded = "User successfully added";
        public const string UserUpdated = "User successfully updated";
        public const string UserAlreadyExists = "User already exists";
        public const string UserEmailAlreadyExists = "This email is already exists";
        public const string UserUsernameAlreadyExists = "This username is already exists";
        public const string UserNotFound = "User not found";
        public const string UserLogged = "User successfully login";
        public const string UserRegistered = "User successfully registered";
        public const string UserWrongPassword = "Wrong password";

        public const string TokenCreated = "Token created";

    }
}
