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
    public class FoodBusiness : IFoodBusiness
    {
        private readonly IConfiguration _configuration;

        private FoodRepository _res; // Không cần khởi tạo ở đây

        public FoodBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new FoodRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }
        public List<FoodModel> GetFood()
        {
            return _res.GetFood();
        }
        public FoodModel GetFoodbyID(int id)
        {
            return _res.GetFoodbyID(id);
        }
    }
}
