using CMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMS.Backend.Controllers
{
    public class PostController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Constructor Injection
        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy dữ liệu từ Database
        // Tham số 'id' được truyền vào từ URL (ví dụ: /Post/Index/5)
        public IActionResult Index(int? id)
        {
            var query = _context.Posts
          .Include(p => p.Category)
          .OrderByDescending(p => p.CreatedDate)
          .AsQueryable();

            // Nếu có id thì mới lọc
            if (id.HasValue)
            {
                query = query.Where(p => p.CategoryId == id.Value);
            }

            var posts = query.ToList();

            return View(posts);
        }


        // Chi tiết bài viết
        public IActionResult Details(int id)
        {
            // 1. Truy vấn bài viết theo ID
            // Sử dụng .Include(p => p.Category) để lấy kèm thông tin Danh mục (Join bảng)
            var post = _context.Posts
                .Include(p => p.Category)
                .FirstOrDefault(p => p.Id == id);

            // 2. Kiểm tra nếu không tìm thấy bài viết (tránh lỗi màn hình trắng)
            if (post == null)
            {
                return NotFound(); // Trả về trang lỗi 404
            }

            // 3. Truyền dữ liệu sang View
            return View(post);
        }

    }
}



//using Microsoft.AspNetCore.Mvc;
//using CMS.Data.Entities; // Quan trọng: Phải có dòng này để dùng lớp Post

//namespace CMS.Backend.Controllers
//{
//    public class PostController : Controller
//    {
//        // Hàm Index: Hiển thị danh sách bài viết mẫu
//        public IActionResult Index()
//        {
//            // 1. Tạo dữ liệu giả (Mock Data) cho Bài viết
//            var posts = new List<Post>
//            {
//                new Post
//                {
//                    Id = 1,
//                    Title = "Lộ trình học ASP.NET Core cho người mới",
//                    Content = "Nội dung bài viết về lộ trình học .NET...",
//                    ImageUrl = "https://via.placeholder.com/150",
//                    CreatedDate = DateTime.Now
//                },
//                new Post
//                {
//                    Id = 2,
//                    Title = "ReactJS và WebAPI: Xu hướng Fullstack 2026",
//                    Content = "Nội dung bài viết về sự kết hợp React và API...",
//                    ImageUrl = "https://via.placeholder.com/150",
//                    CreatedDate = DateTime.Now.AddDays(-1)
//                },
//                new Post
//                {
//                    Id = 3,
//                    Title = "Hướng dẫn cài đặt môi trường Visual Studio",
//                    Content = "Các bước cài đặt công cụ cần thiết cho lập trình viên...",
//                    ImageUrl = "https://via.placeholder.com/150",
//                    CreatedDate = DateTime.Now.AddDays(-2)
//                }
//            };

//            // 2. Gửi danh sách dữ liệu sang View
//            return View(posts);
//        }

//        // Hàm Details: Hiển thị chi tiết một bài viết (Bổ sung  khá giỏi)
//        public IActionResult Details(int id)
//        {
//            // Giả lập tìm bài viết trong Database bằng Id
//            // Trong thực tế tuần sau sẽ là: _context.Posts.Find(id);
//            var post = new Post
//            {
//                Id = id,
//                Title = "Nội dung chi tiết bài viết số " + id,
//                Content = "Đây là nội dung đầy đủ của bài viết mà bạn vừa click vào. Ở đây  có thể viết dài hơn để thấy sự khác biệt với trang danh sách.",
//                ImageUrl = "https://via.placeholder.com/600x300", // Ảnh to hơn
//                CreatedDate = DateTime.Now
//            };

//            if (post == null) return NotFound();

//            return View(post);
//        }
//    }
//}
