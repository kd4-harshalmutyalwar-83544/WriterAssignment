using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WriterAssign.Models;

namespace WriterAssign.Controllers
{
    public class PostController : Controller
    {
        private readonly PostDBContext postDb;
        private const int PageSize = 5;

        public PostController(PostDBContext postDb)
        {
            this.postDb = postDb;
        }

        
        public async Task<IActionResult> Index(int page = 1)
        {
            var totalPosts = await postDb.Posts.CountAsync();
            var posts = await postDb.Posts
                .OrderByDescending(p => p.PublishedDate)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalPosts / PageSize);

            return View(posts);
        }


        public IActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                post.PublishedDate = DateTime.Now;
                await postDb.Posts.AddAsync(post);
                await postDb.SaveChangesAsync();
                TempData["insert_success"] = "Post added successfully.";
                return RedirectToAction("Index");
            }
            return View(post);
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var post = await postDb.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null) return NotFound();

            return View(post);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var post = await postDb.Posts.FindAsync(id);
            if (post == null) return NotFound();

            return View(post);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Post post)
        {
            if (id != post.PostId) return NotFound();

            if (ModelState.IsValid)
            {
                postDb.Update(post);
                await postDb.SaveChangesAsync();
                TempData["update_success"] = "Post updated successfully.";
                return RedirectToAction("Index");
            }
            return View(post);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var post = await postDb.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            if (post == null) return NotFound();

            return View(post);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var post = await postDb.Posts.FindAsync(id);
            if (post != null)
            {
                postDb.Posts.Remove(post);
                await postDb.SaveChangesAsync();
                TempData["delete_success"] = "Post deleted successfully.";
            }
            return RedirectToAction("Index");
        }
    }
}
