using System;
using System.Collections.Generic;
using System.Text;
using BTL_NguyenVanTruong_.Models;

namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IUserRepository
    {
        UserModel Login(string taikhoan, string matkhau, int loaitaikhoan);
    }
}
