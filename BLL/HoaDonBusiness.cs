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
    public partial class HoaDonBusiness : IHoaDonBusiness
    {
        private readonly IConfiguration _configuration;

        private HoaDonRepository _res; // Không cần khởi tạo ở đây

        public HoaDonBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new HoaDonRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

        public bool CreateHoaDon(HoaDonModel model)
        {
            return _res.CreateHoaDon(model);
        }
        public bool UpdateHoaDon(HoaDonModel model)
        {
            return _res.UpdateHoaDon(model);
        }
        public bool DeleteHoaDon(int mhd)
        {
            return _res.DeleteHoaDon(mhd);
        }
        public HoaDonModel GetHoaDonbyID(int id)
        {
            return _res.GetHoaDonbyID(id);
        }
        public List<HoaDonModel> GetHoaDon()
        {
            return _res.GetHoaDon();
        }
        public List<HoaDonModel> SearchHoaDon(string tenkh)
        {
            return _res.SearchHoaDon(tenkh);
        }
    }
}
