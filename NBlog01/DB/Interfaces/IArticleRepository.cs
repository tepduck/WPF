using NBlog01.DB.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Interfaces
{
    public interface IArticleRepository : IRepository<Article>
    {
        public List<Article> GetArticles();
        public List<Article> GetArticleByTitle(string title);
    }
}
