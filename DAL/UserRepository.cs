using DAL.Helper;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class UserRepository : IUserRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }
        public UserModel Login(string taikhoan, string matkhau)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("UserLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TenTaiKhoan", taikhoan);
                    command.Parameters.AddWithValue("@MatKhau", matkhau);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                UserModel userModel = new UserModel
                                {
                                    MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]),
                                    MaLoai = Convert.ToInt32(reader["MaLoai"]),
                                    TenTaiKhoan = reader["TenTaiKhoan"].ToString(),
                                    MatKhau = reader["MatKhau"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Loai = reader["Loai"].ToString(),
                                    

                                };

                                connection.Close(); // Đóng kết nối ở đây để đảm bảo được đóng sau khi đọc dữ liệu.
                                return userModel;
                            }
                        }
                    }
                }
            }

            return null;
        }


    }
}
