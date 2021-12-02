using NBlog01.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.Authentication
{
    public interface IAuth
    {
        public enum Result
        {
            Success,
            LoginProblem
        }

        public Result Register(string username, string password);
        User Login(string username, string password);
    }
}
