using MODEL;
using BLL.Interfaces;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DAL;
using System;

namespace BLL
{
    public class SeatStatusBusiness : ISeatStatusBussiness
    {
        private readonly IConfiguration _configuration;

        private SeatStatusRepository _res; // Không cần khởi tạo ở đây

        public SeatStatusBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new SeatStatusRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }
        public List<SeatStatusModel> GetSeatStatus(int movieId, DateTime showDate, TimeSpan showTime)
        {
            // Thực hiện logic nghiệp vụ, ví dụ kiểm tra đầu vào
            if (movieId <= 0 || showDate == default(DateTime) || showTime == default(TimeSpan))
            {
                throw new ArgumentException("Invalid parameters");
            }

            return _res.GetSeatStatus(movieId, showDate, showTime);
        }
        public bool UpdateSeatStatus(UpdateSeatAdmin model)
        {
            return _res.UpdateSeatStatus(model);
        }
    }
}
