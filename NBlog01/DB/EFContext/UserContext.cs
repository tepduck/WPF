using NBlog01.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.EFContext
{

    internal class UserContext
    {
        private static readonly Lazy<UserContext> Instance = new(() => new UserContext());

        public User User;

        public static UserContext GetInstance()
        {
            return Instance.Value;
        }

        private UserContext()
        {
            User = new User();
        }
    }
}
