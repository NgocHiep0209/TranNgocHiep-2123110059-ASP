/*
    Hoten: Trần Ngọc Hiệp
    masv:2123110059
    ngaytao:14/05/2026
    version:1.0 
 */

using CMS.Data;
using CMS.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization; // Cần thêm namespace này
namespace CMS.Backend.Controllers
{


    [Authorize(Roles = "Admin")] // Chỉ tài khoản có Role là Admin mới được phép vào

    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Hiển thị danh sách User
        public IActionResult Index()
        {
            var users = _context.Users.ToList();

            return View(users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //Thêm Thành Viên Mới
        [HttpPost]
        public IActionResult Create(User model)
        {
            // Kiểm tra xem tên đăng nhập đã tồn tại chưa
            var checkExist = _context.Users.Any(u => u.Username == model.Username);
            if (checkExist)
            {
                ModelState.AddModelError("Username", "Tên đăng nhập này đã có người dùng!");
                return View(model);
            }

            // Lưu User mới vào Database
            _context.Users.Add(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        // update hoặc sữa dữ liệu
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();

            return View(user);
        }

        // POST: Thực hiện lưu thay đổi
        [HttpPost]
        public IActionResult Edit(User model, string NewPassword)
        {
            // 1. Tìm User gốc trong Database để lấy lại mật khẩu cũ nếu cần
            var existingUser = _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == model.Id);

            if (existingUser == null) return NotFound();

            // 2. Xử lý mật khẩu: Nếu nhập mới thì lấy cái mới, nếu trống thì lấy cái cũ
            if (!string.IsNullOrEmpty(NewPassword))
            {
                model.PasswordHash = NewPassword; // Sau này sẽ mã hóa tại đây
            }
            else
            {
                model.PasswordHash = existingUser.PasswordHash;
            }

            // 3. Cập nhật vào Database
            _context.Users.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }


    }
}
//namespace CMS.Backend.Controllers
//{
//    public class UserController : Controller
//    {
//        // Hàm Index: Hiển thị danh sách thành viên quản trị
//        public IActionResult Index()
//        {
//            // 1. Tạo danh sách Người dùng giả (Mock Data)
//            var users = new List<User>
//            {
//                new User
//                {
//                    Id = 1,
//                    Username = "admin_thai",
//                    FullName = "Nguyễn Cao Thái",
//                    Role = "Administrator"
//                },
//                new User
//                {
//                    Id = 2,
//                    Username = "editor_01",
//                    FullName = "Trần Văn Biên Tập",
//                    Role = "Editor"
//                },
//                new User
//                {
//                    Id = 3,
//                    Username = "author_minh",
//                    FullName = "Lê Quang Minh",
//                    Role = "Author"
//                }
//            };

//            // 2. Trả về View kèm theo danh sách người dùng
//            return View(users);
//        }
//    }
//}
