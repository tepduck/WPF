using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Entities
{
    public class Bookmark
    {
        public Bookmark()
        {

        }

        [Key]
        public int id { get; set; }
        public ICollection<ArticlesBookmark> ArticlesBookmarks { get; set; }
    }
}
