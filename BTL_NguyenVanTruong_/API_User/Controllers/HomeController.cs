using BTL_NguyenVanTruong_.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BTL_NguyenVanTruong_.BLL;

namespace BTL_NguyenVanTruong_.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult KhachHang()
        {
            // Khởi tạo đối tượng KhachHangBusiness
            KhachHangBusiness khb = new KhachHangBusiness(_configuration);

            // Gọi phương thức GetAllKhachHangs để lấy danh sách khách hàng
            List<KhachHangModel> danhSachKhachHang = khb.GetAllKhachHangs();
            return View("KhachHang", danhSachKhachHang);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}