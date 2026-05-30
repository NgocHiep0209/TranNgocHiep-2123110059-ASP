/*
    Hoten: Trần Ngọc Hiệp
    masv:2123110059
    ngaytao:14/05/2026
    version:1.0 
 */

using CMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Backend.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Customers.ToList();

            return View(data);
        }
    }
}