using Dragon_BlogReal.Data;
using Dragon_BlogReal.Models;
using Dragon_BlogReal.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Dragon_BlogReal.Controllers
{
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext db;

        public PostsController(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IActionResult> BlogPosts(int? id)
        {
            if(id == null)//check to see if its present
            {
                return NotFound();
            }

            var blog = await db.Blog.FindAsync(id);//check to see if it has an id with the blog
            if(blog == null)
            {
                return NotFound();
            }
            ViewData["BlogName"] = blog.Name;
            ViewData["BlogId"] = blog.Id;

            var blogPosts = await db.Post.Where(p => p.BlogId == id).ToListAsync();
            return View(blogPosts);
        }

        public async Task<IActionResult> ShowPosts(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var blog = await db.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            var posts = db.Post.Where(p => p.BlogId == id);// links post to blog
            return View("Index", await posts.ToListAsync());

        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            var post = db.Post.Include(p => p.Blog);
            return View(await post.ToListAsync());
        }

        // GET: Posts/Details/5
        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await db.Post
                .Include(p => p.Blog)
                .Include(c =>c.Comments).ThenInclude(u => u.BlogUser)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            
            if (post.Image != null)
            {
                ViewData["Image"] = ImageHelper.GetImage(post);
            }

            return View(post);
        }

        // GET: Posts/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create(int? id)
        {
            //Check to see if I have been given the Id of a Blog
            if (id == null)
            {
                //db is the object we use to communicate with the database
                ViewData["BlogId"] = new SelectList(db.Blog, "Id", "Name");
                return View();
               
            }

            var blog = db.Blog.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
           //creates new instance of post
            var newPost = new Post { BlogId = (int)id };
            return View(newPost);
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,BlogId,Body,Abstract,IsPublished")] Post post, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                post.Created = DateTime.Now;
                if(image != null)
                {
                    post.FileName = image.FileName;

                    //This is entry level code that turns our image into a storable format in the database
                    var ms = new MemoryStream();
                    image.CopyTo(ms);
                    post.Image = ms.ToArray();

                    ms.Close();
                    ms.Dispose();
                }
                db.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(db.Blog, "Id", "Name", post.BlogId);
            return View(post);
        }

        // GET: Posts/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var post = await db.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(db.Blog, "Id", "Name", post.BlogId);
            return View(post);
        }


        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BlogId,Created,Title,Abstract,Body,FileName,Image,IsPublished")] Post post, IFormFile image)
        {
            if (id != post.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null)
                    {
                        post.FileName = image.FileName;

                        //This is entry level code that turns our image into a storable format in the database
                        var ms = new MemoryStream();
                        image.CopyTo(ms);
                        post.Image = ms.ToArray();
                        
                        ms.Close();
                        ms.Dispose();
                    }

                    post.Updated = DateTime.Now;
                    db.Update(post);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(db.Blog, "Id", "Id", post.BlogId);
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await db.Post
                .Include(p => p.Blog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await db.Post.FindAsync(id);
            db.Post.Remove(post);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return db.Post.Any(e => e.Id == id);
        }
    }
}
