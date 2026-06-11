using CMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/customers
        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _context.Customers
                .Select(x => new
                {
                    x.Id,
                    x.FullName,
                    x.Email,
                    x.Phone,
                    x.Address
                })
                .ToList();

            return Ok(customers);
        }

        // GET: api/customers/1
        [HttpGet("{id}")]
        public IActionResult GetDetail(int id)
        {
            var customer = _context.Customers
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.Id,
                    x.FullName,
                    x.Email,
                    x.Phone,
                    x.Address
                })
                .FirstOrDefault();

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng"
                });
            }

            return Ok(customer);
        }
    }
}