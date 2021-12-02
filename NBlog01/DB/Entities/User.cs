using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBlog01.DB.Entities
{
    public class User
    {
        public User()
        {
            role = "user";
        }

        public User(string _username, string _password)
        {
            username = _username;
            password = _password;
            role = "user";
        }

        [Key]
        public int id { get; set; }

        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string role { get; set; }
    }
}
