using System;
using System.Collections.Generic;
using System.Text;
using BTL_NguyenVanTruong_.Models;
namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IKhachHangRepository
    {
        bool AddKH(KhachHangModel model);
        bool UpdateKH(KhachHangModel model);
        KhachHangModel GetDataKHByID(string id);
        bool DeleteKH(KhachHangModel model);
        //List<AdminModel> Search(int pageIndex, int pageSize, out long total, string hoten, string diachi);
    }
}
