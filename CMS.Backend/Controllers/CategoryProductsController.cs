using CMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _context.CategoriesProducts
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description
                })
                .ToList();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            var data = _context.CategoriesProducts
                .FirstOrDefault(x => x.Id == id);

            if (data == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy danh mục"
                });
            }

            return Ok(data);
        }
    }
}