using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Dragon_BlogReal.Models;

namespace Dragon_BlogReal.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlogUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Dragon_BlogReal.Models.Blog> Blog { get; set; }
        public DbSet<Dragon_BlogReal.Models.Comment> Comment { get; set; }
        public DbSet<Dragon_BlogReal.Models.Post> Post { get; set; }
        public DbSet<Dragon_BlogReal.Models.Tag> Tag { get; set; }
    }
}
