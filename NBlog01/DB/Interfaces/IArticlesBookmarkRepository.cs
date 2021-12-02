using NBlog01.DB.Entities;
using NBlog01.DB.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Interfaces
{
    public interface IArticlesBookmarkRepository : IRepository<ArticlesBookmark>
    {
        public List<ArticlesBookmark> GetArticlesBookmarks(int id);
        public ArticlesBookmark GetBookmarkByArticleId(int id, int userId);
        public int GetId(ArticlesBookmark articlesBookmark);
        public int GetUserId(ArticlesBookmark articlesBookmark);
    }
}
