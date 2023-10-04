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
        public UserModel Login(string taikhoan, string matkhau, int loaitaikhoan)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("CheckLogin", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@taikhoan", taikhoan);
                        command.Parameters.AddWithValue("@matkhau", matkhau);
                        command.Parameters.AddWithValue("@loaitaikhoan", loaitaikhoan); 

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    UserModel userModel = new UserModel();
                                    {
                                        userModel.MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]);
                                        userModel.LoaiTaiKhoan = Convert.ToInt32(reader["LoaiTaiKhoan"]);
                                        userModel.TenTaiKhoan = reader["TenTaiKhoan"].ToString();
                                        userModel.MatKhau = reader["MatKhau"].ToString();
                                        userModel.Email = reader["Email"].ToString();
                                        userModel.Token = reader["Token"].ToString()
                                    };

                                    return userModel;
                                }
                            }
                        }
                    }
                }

                return null; // Trả về null nếu không có dữ liệu hợp lệ
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi : " + ex.Message);
                return null;
            }
        }

    }
}
