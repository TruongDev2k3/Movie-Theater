using DAL.Helper;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace BTL_NguyenVanTruong_.DAL
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
                                    LoaiTaiKhoan = Convert.ToInt32(reader["LoaiTaiKhoan"]),
                                    TenTaiKhoan = reader["TenTaiKhoan"].ToString(),
                                    MatKhau = reader["MatKhau"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Token = reader["Token"].ToString()
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
