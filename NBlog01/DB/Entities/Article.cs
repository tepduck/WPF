using NBlog01.DB.EFContext;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Entities
{
    public class Article
    {
        public Article()
        {
            author = UserContext.GetInstance().User.username;
        }

        public Article(string _title, string _category, string _descrpt, string _comment, string _image)
        {
            title = _title;
            category = _category;
            descrpt = _descrpt;
            comment = _comment;
            image = _image;
            author = UserContext.GetInstance().User.username;
        }

        [Key]
        public int id { get; set; }

        [Required]
        public string title { get; set; }
        [Required]
        public string category { get; set; }
        [Required]
        public string descrpt { get; set; }
       
        public string author { get; set; }
        public string comment { get; set; }
        public string image { get; set; }
    }
}
