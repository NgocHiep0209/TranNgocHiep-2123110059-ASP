using CMS.Backend.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Microsoft.EntityFrameworkCore;
using CMS.Data; // Thư mục chứa DbContext [cite: 568]
using System.Linq;

namespace CMS.Backend.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // LINQ: Lấy 3 bài viết mới nhất
            var latestPosts = _context.Posts
                             .Include(p => p.Category)
                             .OrderByDescending(p => p.CreatedDate)
                             .Take(3)
                             .ToList();

            return View(latestPosts);
        }
    }
}