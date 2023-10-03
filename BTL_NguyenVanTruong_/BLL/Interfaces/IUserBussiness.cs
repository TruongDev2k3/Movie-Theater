using BTL_NguyenVanTruong_.Models;
namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IUserBussiness
    {
        UserModel Login(string taikhoan, string matkhau, int loaitaikhoan);
    }
}
