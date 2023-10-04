using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL;
using Microsoft.AspNetCore.Authorization;


namespace BTL_NguyenVanTruong_.API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _res;
        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            UserBussiness khb = new UserBussiness(_res,_configuration);
            // Gọi hàm Login từ service hoặc repository của bạn
            var user = khb.Login(model.Username, model.Password, model.LoaiTaiKhoan);

            if (user != null)
            {
                return Ok(new { Token = user.Token });
            }
            else
            {
                return Unauthorized(new { Message = "Tên đăng nhập hoặc mật khẩu không đúng." });
            }
        }
    }
}
