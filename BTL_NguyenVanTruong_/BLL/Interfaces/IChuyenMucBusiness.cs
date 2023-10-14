using BTL_NguyenVanTruong_.Models;
namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IChuyenMucBusiness
    {
        bool CreateChuyenMuc(ChuyenMucModel model);
        bool UpdateChuyenMuc(ChuyenMucModel model);
        ChuyenMucModel GetCMbyID(int mcm);
        bool DeleteChuyenMuc(int mcm);
        List<ChuyenMucModel> GetChuyenMuc();
    }
}
