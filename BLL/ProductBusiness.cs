using MODEL;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Collections.Generic;
using DAL;

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
        public List<ProductsModel> SearchProduct(string tensp)
        {
            return _res.SearchProduct(tensp);
        }
        //public List<KhachHangModel> SearchKhachHang(int pageIndex, int pageSize, out long total, string tenkh, string diachi)
        //{
        //    return _res.SearchKhachHang(pageIndex, pageSize, out total, tenkh, diachi);
        //}
    }
}
