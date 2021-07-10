using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dragon_BlogReal.Data;
using Dragon_BlogReal.Models;
using Microsoft.AspNetCore.Identity;
using SQLitePCL;
using Microsoft.AspNetCore.Authorization;

namespace Dragon_BlogReal.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly UserManager<BlogUser> _userManger;
        public CommentsController(ApplicationDbContext context, UserManager<BlogUser> manager)
        {
            db = context;
            //_userManager = manager;
        }

        // GET: Comments
        [Authorize(Roles ="Admin, Moderator")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = db.Comment.Include(c => c.BlogUser).Include(c => c.Post);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await db.Comment
                .Include(c => c.BlogUser)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }
            

            return View(comment);
        }
        [Authorize]
        // GET: Comments/Create
        public IActionResult Create()//renders the view
        {
          
            ViewData["BlogUserId"] = new SelectList(db.Set<BlogUser>(), "Id", "Id");
            ViewData["PostId"] = new SelectList(db.Post, "Id", "Id");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogUserId,PostId,Content")] Comment comment, string commentContent)
        {
            if (ModelState.IsValid)
            {
                //comment.BlogUserId = _userManager.GetUserId(user);

                comment.Created = DateTime.Now;
                //The Id of the logged in user can be retrieved programmatically 
                var aurthor = await db.Users.FirstOrDefaultAsync(u => u.UserName == User.Identity.Name);//this is how I get data from a database
                comment.BlogUserId = aurthor.Id;

                db.Add(comment);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogUserId"] = new SelectList(db.Set<BlogUser>(), "Id", "Name", comment.BlogUserId);
            ViewData["PostId"] = new SelectList(db.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await db.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(db.Blog, "Id", "Name", comment.BlogUserId);

            //ViewData["BlogUserId"] = new SelectList(db.Set<BlogUser>(), "Id", "Id", comment.BlogUserId);
            ViewData["PostId"] = new SelectList(db.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PostId,BlogUserId,Content,Created,Updated")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(comment);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["BlogUserId"] = new SelectList(db.Set<BlogUser>(), "Id", "Name", comment.BlogUserId);
            ViewData["PostId"] = new SelectList(db.Post, "Id", "Id", comment.PostId);
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await db.Comment
                .Include(c => c.BlogUser)
                .Include(c => c.Post)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await db.Comment.FindAsync(id);
            db.Comment.Remove(comment);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return db.Comment.Any(e => e.Id == id);
        }
    }
}
