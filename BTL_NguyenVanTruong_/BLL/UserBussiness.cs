using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace BTL_NguyenVanTruong_.BLL
{
    public partial class UserBussiness : IUserBussiness
    {
        public static IConfiguration _configuration { get; set; }
        private readonly IUserRepository _res;
        private string secret;

        public UserBussiness(IUserRepository res, IConfiguration configuration)
        {
            _res = res;
            secret = configuration["AppSettings:Secret"];
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }
        public UserModel Login(string taikhoan, string matkhau, int loaitaikhoan)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("CheckLogin", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TenTaiKhoan", taikhoan);
                    command.Parameters.AddWithValue("@MatKhau", matkhau);
                    command.Parameters.AddWithValue("@LoaiTaiKhoan", loaitaikhoan);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            UserModel user = new UserModel
                            {
                                MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]),
                                LoaiTaiKhoan = Convert.ToInt32(reader["LoaiTaiKhoan"]),
                                TenTaiKhoan = reader["TenTaiKhoan"].ToString(),
                                Email = reader["Email"].ToString(),
                                
                            };

                            // Tạo và gán giá trị cho token 
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var key = Encoding.ASCII.GetBytes(secret);
                            var tokenDescriptor = new SecurityTokenDescriptor
                            {
                                Subject = new ClaimsIdentity(new Claim[]
                                {
                            new Claim(ClaimTypes.Name, user.TenTaiKhoan.ToString()),
                            new Claim(ClaimTypes.Email, user.Email)
                                }),
                                Expires = DateTime.UtcNow.AddDays(7),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Aes128CbcHmacSha256)
                            };
                            var Token = tokenHandler.CreateToken(tokenDescriptor);
                            user.Token = tokenHandler.WriteToken(Token);

                            return user;
                        }
                    }
                }
            }

            // Nếu không tìm thấy người dùng
            return null;
        }

    }
}
