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
    public class BookmarkRepository : Repository<Bookmark>, IBookmarkRepository
    {
        public BookmarkRepository(ArticleContext context) : base(context)
        {

        }

        public ArticleContext ArticleContext
        {
            get { return _context as ArticleContext; }
        }

        public List<Bookmark> GetBookmarks()
        {
            return ArticleContext.Bookmarks
                .Distinct()
                .ToList();
        }
    }
}
