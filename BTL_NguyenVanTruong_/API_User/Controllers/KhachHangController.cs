using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;

namespace BTL_NguyenVanTruong_.API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public KhachHangController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("AddKhachHang")]
        [HttpPost]
        public IActionResult AddKhachHang([FromBody] KhachHangModel model)
        {
            // Khởi tạo đối tượng KhachHangRepository
            KhachHangRepository khachHangRepository = new KhachHangRepository(_configuration);

            bool result = khachHangRepository.AddKH(model);

            if (result)
            {
                return Ok("Khách hàng đã được thêm thành công.");
            }
            else
            {
                return BadRequest("Lỗi khi thêm khách hàng.");
            }
        }
    }
}
