using NBlog01.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public List<User> GetUsers();
        public User GetUserbyName(string name);
    }
}
