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
    public class DayshowtimeBusiness : IDayshowtimeBusiness
    {
        private readonly IConfiguration _configuration;

        private DayshowtimeRepository _res; // Không cần khởi tạo ở đây

        public DayshowtimeBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new DayshowtimeRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

       
        public List<DayshowModel> GetDayshowtime()
        {
            return _res.GetDayshowtime();
        }
    }
}
