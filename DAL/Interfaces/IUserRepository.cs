using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IUserRepository
    {
        UserModel Login(string taikhoan, string matkhau);
    }
}
