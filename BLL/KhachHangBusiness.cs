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
    public partial class KhachHangBusiness : IKhachHangBusiness
    {
        private readonly IConfiguration _configuration;
       
        private KhachHangRepository _res; // Không cần khởi tạo ở đây

        public KhachHangBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new KhachHangRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }
        
        public bool CreateCustomer(CustomerModel model)
        {
            return _res.CreateCustomer(model);
        }
        public bool UpdateCustomer(CustomerModel model)
        {
            return _res.UpdateCustomer(model);
        }
        public bool DeleteCustomer(int id)
        {
            return _res.DeleteCustomer(id);
        }
        public CustomerModel GetCustomerByID(int id)
        {
            return _res.GetCustomerByID(id);
        }
        public List<CustomerModel> GetAllKhachHangs()
        {
            return _res.GetAllKhachHangs();
        }
        public List<CustomerModel> SearchKhachHang(string tukhoa)
        {
            return _res.SearchKhachHang(tukhoa);
        }
    }
}
