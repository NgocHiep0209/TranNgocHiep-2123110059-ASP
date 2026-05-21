using CMS.Data;
using CMS.Data.Entities; // Kết nối tới lớp dữ liệu bạn vừa tạo
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace CMS.Backend.Controllers
{
    public class CategoryProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.CategoriesProducts.ToList();

            return View(data);
        }
    }
}