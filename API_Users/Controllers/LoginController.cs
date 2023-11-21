using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using BTL_NguyenVanTruong_.BLL;
using MODEL;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication;

namespace BTL_NguyenVanTruong_.API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IUserBussiness _userBusiness;
        public LoginController(IUserBussiness userBusiness)
        {
            _userBusiness = userBusiness;           
        }
        //[AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AuthenticateModel model)
        {
            var user = _userBusiness.Login(model.Username, model.Password);

            if (user != null)
            {
                // Gán vai trò dựa trên thông tin người dùng (ví dụ: ADMIN hoặc USER)
                var roles = new List<string> { user.Loai.ToString() };

                // Tạo claims cho vai trò
                var claims = roles.Select(role => new Claim(ClaimTypes.Role, role));

                // Tạo identity và principal
                var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                // Gán principal cho người dùng
                HttpContext.SignInAsync(principal);

                return Ok(new { taikhoan = user.TenTaiKhoan, email = user.Email, Loai = user.Loai, token = user.Token });
            }
            else
            {
                return Unauthorized(new { Message = "Tên đăng nhập hoặc mật khẩu không đúng." });
            }
        }

    }
}
