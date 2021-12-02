using NBlog01.DB.EFContext;
using NBlog01.DB.Entities;
using NBlog01.DB.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Repositories
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        public ArticleRepository(ArticleContext context) : base(context)
        {

        }

        public ArticleContext ArticleContext
        {
            get { return _context as ArticleContext; }
        }

        public List<Article> GetArticleByTitle(string title)
        {
            return ArticleContext.Articles
               .AsNoTracking()
               .Where(c => c.title == title)
               .ToList();
        }

        public List<Article> GetArticles()
        {
            return ArticleContext.Articles
                .Distinct()
                .ToList();
        }

        public int GetArticleId(Article article)
        {
            return article.id;
        }

        public Article GetArticlebyId(int id)
        {
            return ArticleContext.Articles
                .AsNoTracking()
                .First(c => c.id == id);
        }
    }
}
