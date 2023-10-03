using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using BTL_NguyenVanTruong_.DAL;
namespace BTL_NguyenVanTruong_.BLL
{
    public partial class UserBussiness : IUserBussiness
    {
        private readonly IUserRepository _res;
        private readonly string secret;

        public UserBussiness(IUserRepository res, IConfiguration configuration)
        {
            _res = res;
            secret = configuration["AppSettings:Secret"];
        }

        public UserModel Login(string taikhoan, string matkhau, int loaitaikhoan)
        {
            return _res.Login(taikhoan,matkhau,loaitaikhoan);
        }
    }
}
