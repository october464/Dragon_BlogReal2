using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Models
{
    public class BlogPosts
    {
        #region Keys
        public int Id { get; set; }
        [Display(Name = "Blog")]
        public int BlogId { get; set; }
        #endregion

        #region Blog Properties
        public string Title { get; set; }
        public string Abstract{ get; set; }

        public string Body { get; set; }
        public string Slug { get; set; }
        public byte[] Image { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public bool IsPublished { get; set; }
        #endregion

        #region Navigation
        public virtual ICollection<BlogPosts> BlogPost{ get; set; }
        public BlogPosts()
        {
            BlogPost = new HashSet<BlogPosts>();
        }
        #endregion


    }
}
