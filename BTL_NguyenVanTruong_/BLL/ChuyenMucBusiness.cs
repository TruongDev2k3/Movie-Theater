using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using BTL_NguyenVanTruong_.DAL;
namespace BTL_NguyenVanTruong_.BLL
{
    public partial class ChuyenMucBusiness : IChuyenMucBusiness
    {
        private readonly IConfiguration _configuration;

        private ChuyenMucRepository _res; // Không cần khởi tạo ở đây

        public ChuyenMucBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new ChuyenMucRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

        public bool CreateChuyenMuc(ChuyenMucModel model)
        {
            return _res.CreateChuyenMuc(model);
        }
        public bool UpdateChuyenMuc(ChuyenMucModel model)
        {
            return _res.UpdateChuyenMuc(model);
        }
        public bool DeleteChuyenMuc(int mcm)
        {
            return _res.DeleteChuyenMuc(mcm);
        }
        public ChuyenMucModel GetCMbyID(int mcm)
        {
            return _res.GetCMbyID(mcm);
        }
        public List<ChuyenMucModel> GetChuyenMuc()
        {
            return _res.GetChuyenMuc();
        }

    }
}
