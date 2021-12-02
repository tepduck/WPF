using NBlog01.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Interfaces
{
    public interface IBookmarkRepository : IRepository<Bookmark>
    {
        public List<Bookmark> GetBookmarks();
      //  public Bookmark AddBookmark(Article article, Bookmark bookmark, User user);
    }
}
