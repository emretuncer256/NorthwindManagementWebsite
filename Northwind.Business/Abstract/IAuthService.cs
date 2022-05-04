using Northwind.Core.Entities.Concrete;
using Northwind.Core.Utilities.Results.Abstract;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IDataResult<User> UpdateUser(UserForUpdateDto userForUpdateDto, string password);
        IResult UserEmailExists(string email);
        IDataResult<List<OperationClaim>> GetUserClaims(User user);
        IDataResult<User> GetUserByEmail(string email);
    }
}
