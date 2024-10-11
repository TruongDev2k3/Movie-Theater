using BLL.Interfaces;
using DAL;
using Microsoft.Extensions.Configuration;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PremiereBusiness : IPremiereBusiness
    {
        private readonly IConfiguration _configuration;

        private PremiereRepository _res; // Không cần khởi tạo ở đây

        public PremiereBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new PremiereRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

        
        public List<PremiereModel> GetPremiere()
        {
            return _res.GetPremiere();
        }
    }
}
