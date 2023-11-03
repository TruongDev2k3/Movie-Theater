using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IUserBussiness
    {
        UserModel Login(string taikhoan, string matkhau);
    }
}
