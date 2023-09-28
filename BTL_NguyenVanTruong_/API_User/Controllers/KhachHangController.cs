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

        // API LẤY DANH SÁCH KHÁCH HÀNG
        [Route("GetAllKhachHangs")]
        [HttpGet]
        public IActionResult GetAllKhachHangs()
        {
            try
            {
                // Khởi tạo đối tượng KhachHangBusiness
                KhachHangBusiness khb = new KhachHangBusiness(_configuration);

                // Gọi phương thức GetAllKhachHangs để lấy danh sách khách hàng
                List<KhachHangModel> danhSachKhachHang = khb.GetAllKhachHangs();

                if (danhSachKhachHang != null && danhSachKhachHang.Count > 0)
                {
                    return Ok(danhSachKhachHang);
                }
                else
                {
                    return NotFound("Không tìm thấy danh sách khách hàng hoặc có lỗi xảy ra.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách khách hàng: " + ex.Message);
                return BadRequest("Lỗi khi lấy danh sách khách hàng.");
            }
        }

        // API THÊM KHÁCH HÀNG
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


        // API XÓA KHÁCH HÀNG
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

        // API LẤY THÔNG TIN KHÁCH HÀNG THEO MÃ KHÁCH HÀNG
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


        // API CẬP NHẬP THÔNG TIN KHÁCH HÀNG
        [HttpPut]
        [Route("UpdateKhachHang")]
        public IActionResult UpdateKhachHang([FromBody] KhachHangModel model)
        {
            KhachHangBusiness khb = new KhachHangBusiness(_configuration);

            bool result = khb.UpdateKH(model);

            if (result)
            {
                return Ok("Khách hàng đã được cập nhật thành công.");
            }
            else
            {
                return BadRequest("Lỗi khi cập nhật khách hàng.");
            }
        }


        //API TÌM KIẾM THÔNG TIN KHÁCH HÀNG
        [HttpGet]
        [Route("SearchKhachHang")]
        public IActionResult SearchKhachHang(int pageIndex, int pageSize, string tenkh, string diachi)
        {
            try
            {
                long total;
                KhachHangBusiness khb = new KhachHangBusiness(_configuration);

                // Gọi phương thức GetAllKhachHangs để lấy danh sách khách hàng
                List<KhachHangModel> danhSachKhachHang = khb.SearchKhachHang(pageIndex, pageSize, out total, tenkh, diachi);

                if (danhSachKhachHang != null && danhSachKhachHang.Count > 0)
                {
                    var result = new
                    {
                        Total = total,
                        Data = danhSachKhachHang
                    };
                    return Ok(result);
                }
                else
                {
                    return NotFound("Không tìm thấy khách hàng nào phù hợp hoặc có lỗi xảy ra.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tìm kiếm khách hàng: " + ex.Message);
                return BadRequest("Lỗi khi tìm kiếm khách hàng.");
            }
        }

    }
}
