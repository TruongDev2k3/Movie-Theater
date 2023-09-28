using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL;

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
             KhachHangBusiness khb = new KhachHangBusiness(_configuration);

            bool result = khb.AddKH(model);

            if (result)
            {
                return Ok("Khách hàng đã được thêm thành công.");
            }
            else
            {
                return BadRequest("Lỗi khi thêm khách hàng.");
            }
        }
        [Route("DeleteKH/{id}")]
        [HttpDelete]
        public IActionResult DeleteKH(int id)
        {
            // Khởi tạo đối tượng KhachHangRepository
            KhachHangBusiness khb = new KhachHangBusiness(_configuration);

            bool result = khb.DeleteKH(id);

            if (result)
            {
                return Ok("Khách hàng đã được xóa thành công.");
            }
            else
            {
                return BadRequest("Lỗi khi xóa khách hàng.");
            }
        }

        [Route("GetKhachHangByID/{id}")]
        [HttpGet]
        public IActionResult GetKhachHangByID(int id)
        {
            // Khởi tạo đối tượng KhachHangBusiness
            KhachHangBusiness khb = new KhachHangBusiness(_configuration);

            // Gọi phương thức GetDataKHByID để lấy thông tin khách hàng
            KhachHangModel khachHang = khb.GetDataKHByID(id);

            if (khachHang != null)
            {
                return Ok(khachHang);
            }
            else
            {
                return NotFound("Khách hàng không tồn tại.");
            }
        }
    }
}
