using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ArticleContext context) : base(context)
        {
        }

        public ArticleContext ArticleContext
        {
            get { return _context as ArticleContext; }
        }

        public List<User> GetUsers()
        {
            return ArticleContext.Users
                .Distinct()
                .ToList();
        }

        public User GetUserbyName(string name)
        {
            User user = ArticleContext.Users
                .FirstOrDefault(c => c.username == name);
            return user;
        }
    }
}
