using Microsoft.AspNet.Identity;
using NBlog01.DB.Entities;
using NBlog01.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.Authentication
{
    public class Auth : IAuth
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;

        public Auth(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = new PasswordHasher();
        }

        public User Login(string username, string password)
        {
            User user = _unitOfWork.Users.GetUserbyName(username);
            try
            {
                PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(user.password, password);
                if (verificationResult != PasswordVerificationResult.Success)
                    throw new Exception();
                return user;
            }
            catch
            {
                return null;
            }
        }

        public IAuth.Result Register(string username, string password)
        {
            IAuth.Result result = IAuth.Result.Success;
            if (_unitOfWork.Users.GetUserbyName(username) != null)
                result = IAuth.Result.LoginProblem;
            else if(result == IAuth.Result.Success)
            {
                string hashedPassword = _passwordHasher.HashPassword(password);
                User user = new User(username, hashedPassword);
                _unitOfWork.Users.Add(user);
                _unitOfWork.Save();
            }

            return result;
        }
    }
}
