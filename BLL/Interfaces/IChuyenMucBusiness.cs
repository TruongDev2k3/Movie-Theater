using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IChuyenMucBusiness
    {
        bool CreateChuyenMuc(ChuyenMucModel model);
        bool UpdateChuyenMuc(ChuyenMucModel model);
        ChuyenMucModel GetCMbyID(int mcm);
        bool DeleteChuyenMuc(int mcm);
        List<ChuyenMucModel> GetChuyenMuc();
        List<ChuyenMucModel> SearchChuyenMuc(string tencm);
    }
}
