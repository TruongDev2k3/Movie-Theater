using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL;
using Microsoft.AspNetCore.Mvc;

namespace BTL_NguyenVanTruong_.API_User.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        public IActionResult UserLogin()
        {
            return View();
        }
    }
}
