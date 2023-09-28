﻿using BTL_NguyenVanTruong_.Models;

namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IKhachHangBusiness
    {
        bool AddKH(KhachHangModel model);
        bool UpdateKH(KhachHangModel model);
        KhachHangModel GetDataKHByID(int id);
        bool DeleteKH(int id);
    }
}
