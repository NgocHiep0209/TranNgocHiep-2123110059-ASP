using CMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/orderdetails
        [HttpGet]
        public IActionResult GetAll()
        {
            var details = _context.OrderDetails
                .Include(x => x.Product)
                .Include(x => x.Order)
                .Select(x => new
                {
                    x.Id,
                    x.OrderId,
                    ProductName = x.Product.Name,
                    x.Quantity,
                    x.UnitPrice,
                    TotalPrice = x.Quantity * x.UnitPrice
                })
                .ToList();

            return Ok(details);
        }

        // GET: api/orderdetails/1
        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            var detail = _context.OrderDetails
                .Include(x => x.Product)
                .Include(x => x.Order)
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.OrderId,
                    ProductName = x.Product.Name,
                    x.Quantity,
                    x.UnitPrice,
                    TotalPrice = x.Quantity * x.UnitPrice
                })
                .FirstOrDefault();

            if (detail == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy chi tiết đơn hàng"
                });
            }

            return Ok(detail);
        }
    }
}