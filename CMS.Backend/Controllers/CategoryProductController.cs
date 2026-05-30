/*
    Hoten: Trần Ngọc Hiệp
    masv:2123110059
    ngaytao:14/05/2026
    version:1.0 
 */

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