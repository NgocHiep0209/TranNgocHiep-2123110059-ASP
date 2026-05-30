/*
    Hoten: Trần Ngọc Hiệp
    masv:2123110059
    ngaytao:14/05/2026
    version:1.0 
 */

using CMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Backend.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Orders
                               .Include(o => o.Customer)
                               .ToList();

            return View(data);
        }
    }
}