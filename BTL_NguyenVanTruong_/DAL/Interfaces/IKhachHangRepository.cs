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
        KhachHangModel GetDataKHByID(int id);
        bool DeleteKH(int id);
        List<KhachHangModel> GetAllKhachHangs();
        List<KhachHangModel> SearchKhachHang(int pageIndex, int pageSize, out long total, string tenkh, string diachi);
    }
}
