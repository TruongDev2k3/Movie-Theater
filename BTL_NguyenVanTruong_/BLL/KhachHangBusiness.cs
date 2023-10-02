using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using BTL_NguyenVanTruong_.DAL;

namespace BTL_NguyenVanTruong_.BLL
{
    public partial class KhachHangBusiness : IKhachHangBusiness
    {
        private readonly IConfiguration _configuration;
       
        private KhachHangRepository _res; // Không cần khởi tạo ở đây

        public KhachHangBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new KhachHangRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }
        
        public bool CreateCustomer(KhachHangModel model)
        {
            return _res.CreateCustomer(model);
        }
        public bool UpdateKH(KhachHangModel model)
        {
            return _res.UpdateKH(model);
        }
        public bool DeleteKH(int id)
        {
            return _res.DeleteKH(id);
        }
        public KhachHangModel GetDataKHByID(int id)
        {
            return _res.GetDataKHByID(id);
        }
        public List<KhachHangModel> GetAllKhachHangs()
        {
            return _res.GetAllKhachHangs();
        }
        public List<KhachHangModel> SearchKhachHang(int pageIndex, int pageSize, out long total, string tenkh, string diachi)
        {
            return _res.SearchKhachHang(pageIndex, pageSize, out total, tenkh, diachi);
        }
    }
}
