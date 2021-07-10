using Dragon_BlogReal.Data;
using Dragon_BlogReal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Helper
{
    public class BlogHelper
    {
        public static List<Blog> GetBlogs(ApplicationDbContext db)
        {
            return db.Blog.ToList();
        }
    }
}
