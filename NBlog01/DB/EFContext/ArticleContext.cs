using NBlog01.DB.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.EFContext
{
    public class ArticleContext : DbContext
    {
        public ArticleContext() : base("KKD_DBEntities")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<ArticlesBookmark> ArticlesBookmarks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
