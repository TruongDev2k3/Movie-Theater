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
    public class TheaterBusiness : ITheaterBusiness
    {
        private readonly IConfiguration _configuration;

        private TheaterRepository _res; // Không cần khởi tạo ở đây

        public TheaterBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new TheaterRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

        public bool CreateTheater(TheaterModel model)
        {
            return _res.CreateTheater(model);
        }
        public bool UpdateTheater(TheaterModel model)
        {
            return _res.UpdateTheater(model);
        }
        public bool DeleteTheater(int mcm)
        {
            return _res.DeleteTheater(mcm);
        }
        public TheaterModel GetTheaterbyID(int mtk)
        {
            return _res.GetTheaterbyID(mtk);
        }
        public List<TheaterModel> GetTheater()
        {
            return _res.GetTheater();
        }
    }
}
