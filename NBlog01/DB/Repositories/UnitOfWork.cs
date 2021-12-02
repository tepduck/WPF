using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ArticleContext _context;

        public UnitOfWork(ArticleContext context)
        {
            _context = context;
            Articles = new ArticleRepository(_context);
            Bookmarks = new BookmarkRepository(_context);
            ArticlesBookmark = new ArticlesBookmarkRepository(_context);
            Users = new UserRepository(_context);
        }
        public IArticleRepository Articles { get; private set; }
        public IBookmarkRepository Bookmarks { get; private set; }
        public IArticlesBookmarkRepository ArticlesBookmark { get; private set; }
        public IUserRepository Users { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
