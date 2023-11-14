using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IHoaDonRepository
    {
        bool CreateHoaDon(HoaDonModel model);
        bool UpdateHoaDon(HoaDonModel model);
        HoaDonModel GetHoaDonbyID(int mhd);
        bool DeleteHoaDon(int mhd);
        List<HoaDonModel> GetHoaDon();
        List<HoaDonModel> SearchHoaDon(string tenkh);
    }
}
