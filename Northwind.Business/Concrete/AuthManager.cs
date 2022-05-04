using Northwind.Business.Abstract;
using Northwind.Business.Constants;
using Northwind.Core.Entities.Concrete;
using Northwind.Core.Utilities.Results;
using Northwind.Core.Utilities.Results.Abstract;
using Northwind.Core.Utilities.Security.Hashing;
using Northwind.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public IDataResult<List<OperationClaim>> GetUserClaims(User user)
        {
            var claims = _userService.GetClaims(user);
            return new SuccessDataResult<List<OperationClaim>>(claims);
        }

        public IDataResult<User> Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Password, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.UserWrongPassword);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.UserLogged);
        }

        public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                Password = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true
            };
            _userService.Add(user);
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<User> GetUserByEmail(string email)
        {
            User user = _userService.GetByMail(email);
            return new SuccessDataResult<User>(user);
        }

        public IResult UserEmailExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult(Messages.UserEmailAlreadyExists);
            }
            return new SuccessResult();
        }

        public IDataResult<User> UpdateUser(UserForUpdateDto userForUpdateDto, string password)
        {
            if (userForUpdateDto.Password == userForUpdateDto.RetypePassword)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
                var user = new User
                {
                    Id = userForUpdateDto.Id,
                    Email = userForUpdateDto.Email,
                    FirstName = userForUpdateDto.FirstName,
                    LastName = userForUpdateDto.LastName,
                    Password = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };
                _userService.Update(user);
                return new SuccessDataResult<User>(user, Messages.UserUpdated);
            }
            return new ErrorDataResult<User>();
        }
    }
}
