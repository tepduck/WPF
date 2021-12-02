using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Repositories
{
    public class ArticlesBookmarkRepository : Repository<ArticlesBookmark>, IArticlesBookmarkRepository
    {
        public ArticlesBookmarkRepository(ArticleContext context) : base(context)
        {

        }

        public ArticleContext ArticleContext
        {
            get { return _context as ArticleContext; }
        }

        public List<ArticlesBookmark> GetArticlesBookmarks(int id)
        {
            return ArticleContext.ArticlesBookmarks
                .Distinct()
                .Where(b => b.UserId == id)
                .ToList();
        }

        public ArticlesBookmark GetBookmarkByArticleId(int id, int userId)
        {
            return ArticleContext.ArticlesBookmarks
                .Where(b => (b.ArticleId == id && b.UserId == userId))
                .FirstOrDefault();
        }

        public int GetId(ArticlesBookmark articlesBookmark)
        {
            return articlesBookmark.ArticleId;
        }

        public int GetUserId(ArticlesBookmark articlesBookmark)
        {
            return articlesBookmark.UserId;
        }
    }
}
