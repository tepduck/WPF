using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IArticleRepository Articles { get; }
        IBookmarkRepository Bookmarks { get; }
        IArticlesBookmarkRepository ArticlesBookmark { get; }
        IUserRepository Users { get; }

        int Save();
    }
}
