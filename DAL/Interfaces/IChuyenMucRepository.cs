using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IChuyenMucRepository
    {
        bool CreateChuyenMuc(ChuyenMucModel model);
        bool UpdateChuyenMuc(ChuyenMucModel model);
        ChuyenMucModel GetCMbyID(int mcm);
        bool DeleteChuyenMuc(int mcm);
        List<ChuyenMucModel> GetChuyenMuc();
        List<ChuyenMucModel> SearchChuyenMuc(string tencm);
    }
}
