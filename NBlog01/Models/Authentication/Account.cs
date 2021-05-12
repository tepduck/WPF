using NBlog01.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.Models.Authentication
{
    public static class Account
    {
        public static Users curUser { get; set; }
        public static bool IsLoggedIn { get; set; }
    }
}
