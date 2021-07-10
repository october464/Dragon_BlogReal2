using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Models
{
    public class Blog
    {
        #region Keys
        public int Id { get; set; }
        #endregion

        #region Blog Properties
        public string Name { get; set; }
        public string URL { get; set; }
        #endregion

        #region Navigation
        //public List<Post> Posts { get; set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Blog()
        {
           Posts = new HashSet<Post>();
        }
        #endregion
    }
}
