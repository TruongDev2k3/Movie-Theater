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
    public class QuangCaoBusiness : IQuangCaoBusiness
    {
        private readonly IConfiguration _configuration;

        private QuangCaoRepository _res; // Không cần khởi tạo ở đây

        public QuangCaoBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new QuangCaoRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

        public bool CreateQuangCao(QuangCaoModel model)
        {
            return _res.CreateQuangCao(model);
        }
        public bool UpdateQuangCao(QuangCaoModel model)
        {
            return _res.UpdateQuangCao(model);
        }
        public bool DeleteQuangCao(int mqc)
        {
            return _res.DeleteQuangCao(mqc);
        }
        public QuangCaoModel GetQCbyID(int mqc)
        {
            return _res.GetQCbyID(mqc);
        }
        public List<QuangCaoModel> GetQuangCao()
        {
            return _res.GetQuangCao();
        }
    }
}
