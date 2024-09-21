using BTL_NguyenVanTruong_.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using DAL.Interfaces;

namespace DAL
{
    public class SeatStatusRepository : ISeatStatusRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public SeatStatusRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }
        public List<SeatStatusModel> GetSeatStatus(int movieId, DateTime showDate, TimeSpan showTime)
        {
            List<SeatStatusModel> seatStatuses = new List<SeatStatusModel>();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("GetMovieSeatStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MovieId", movieId);
                    cmd.Parameters.AddWithValue("@ShowDate", showDate);
                    cmd.Parameters.AddWithValue("@ShowTime", showTime);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SeatStatusModel seatStatus = new SeatStatusModel
                            {
                                Title = reader["Title"].ToString(),
                                ShowDate = Convert.ToDateTime(reader["ShowDate"]),
                                ShowTime = (TimeSpan)reader["ShowTime"],
                                TheaterId = Convert.ToInt32(reader["TheaterId"]),
                                TheaterName = reader["TheaterName"].ToString(),
                                SeatId = Convert.ToInt32(reader["SeatId"]),
                                SeatName = reader["SeatName"].ToString(),
                                SeatStatus = Convert.ToBoolean(reader["SeatStatus"])
                            };
                            seatStatuses.Add(seatStatus);
                        }
                    }
                }
            }

            return seatStatuses;
        }
    }
}
