using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using BTL_NguyenVanTruong_.DAL;
namespace BTL_NguyenVanTruong_.BLL
{
    public partial class ProductBusiness : IProductBusiness
    {
        private readonly IConfiguration _configuration;

        private ProductRepository _res; // Không cần khởi tạo ở đây

        public ProductBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new ProductRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

        public bool CreateProduct(ProductsModel model)
        {
            return _res.CreateProduct(model);
        }
        public bool UpdateProduct(ProductsModel model)
        {
            return _res.UpdateProduct(model);
        }
        public bool DeleteProduct(int masp)
        {
            return _res.DeleteProduct(masp);
        }
        public ProductsModel GetProductByMaSP(int masp)
        {
            return _res.GetProductByMaSP(masp);
        }
        public List<ProductsModel> GetListProduct()
        {
            return _res.GetListProduct();
        }
        public List<ProductsModel> GetListIphoneProduct()
        {
            return _res.GetListIphoneProduct();
        }
        public List<ProductsModel> GetListMacProduct()
        {
            return _res.GetListMacProduct();
        }
        //public List<KhachHangModel> SearchKhachHang(int pageIndex, int pageSize, out long total, string tenkh, string diachi)
        //{
        //    return _res.SearchKhachHang(pageIndex, pageSize, out total, tenkh, diachi);
        //}
    }
}
