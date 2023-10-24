using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;


namespace BTL_NguyenVanTruong_.API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IUserBussiness _userBusiness;
        private IKhachHangBusiness _khb;
        public ValuesController(IUserBussiness userBusiness, IKhachHangBusiness khb)
        {
            _userBusiness = userBusiness;
            _khb = khb;
        }
        //[AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _userBusiness.Login(model.Username, model.Password);

            if (user != null)
            {
                return Ok(new { taikhoan = user.TenTaiKhoan, email = user.Email, Loai = user.Loai , token = user.Token });
            }
            else
            {
                return Unauthorized(new { Message = "Tên đăng nhập hoặc mật khẩu không đúng." });
            }
        }



        [HttpGet("getbyid/{id}")]
        public ActionResult<KhachHangModel> GetCustomerById(int id)
        {
            try
            {
                var khachHang = _khb.GetCustomerByID(id);

                if (khachHang == null)
                {
                    return NotFound("khách hàng không hợp lệ");
                }

                return Ok(khachHang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
