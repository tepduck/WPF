using NBlog01.DB.EFContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Entities
{
    public class ArticlesBookmark
    {
        public ArticlesBookmark()
        {
            UserId = UserContext.GetInstance().User.id;
        }

        public ArticlesBookmark(int bookmarkId, int articleId)
        {
            UserId = UserContext.GetInstance().User.id;
            BookmarkId = bookmarkId;
            ArticleId = articleId;
        }

        public ArticlesBookmark( int articleId)
        {
            UserId = UserContext.GetInstance().User.id;
            ArticleId = articleId;
        }

        public int Id { get; set; }
        public int BookmarkId { get; set; }
        public virtual Bookmark Bookmark { get; set; }
        public int ArticleId { get; set; }
        public virtual Article Article { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
