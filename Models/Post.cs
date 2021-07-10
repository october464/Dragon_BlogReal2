using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Models
{
    //This model represents each individual Blog Post 
    public class Post
    {
        #region Keys
        public int Id { get; set; }
        public int BlogId { get; set; }//Reference the parent 
        #endregion

        #region Post Properties 
        //Describes the Tthings that a blog post should be 
        public string Title { get; set; }
        public string Abstract { get; set; }

        public string Body { get; set; }
        public string Slug { get; set; }

        [Display(Name ="File Name")]
        public string FileName { get; set; }

        //how to get data in to a useable image right inside the get();
        public byte[] Image { get; set; }

        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }

        public bool IsPublished { get; set; }
        #endregion

        #region Navigation
        //In Microsoft Documentation this is written public Type type
        public virtual Blog Blog { get; set; }

        //In Microsoft docs this is public List<T> Types
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        #endregion


        //This tells the code what to do when I type :L
        //var post = new post();

        public Post()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

      
    }
}
