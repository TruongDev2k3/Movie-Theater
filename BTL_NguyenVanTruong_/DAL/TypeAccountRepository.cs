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
    public class TypeAccountRepository : ITypeAccountRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public TypeAccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }

        //LẤY TOÀN BỘ BẢN GHI THÔNG TIN DANH SÁCH KHÁCH HÀNG
        public List<TypeAccountModel> GetTypeAccount()
        {
            List<TypeAccountModel> typeacc = new List<TypeAccountModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối

                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetTypeAcc"; // Tên stored procedure

                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();

                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    TypeAccountModel acc = new TypeAccountModel();
                    {
                        acc.MaLoai = (int)reader["MaLoai"];
                        acc.TenLoai = reader["TenLoai"].ToString();
                        acc.MoTa = reader["MoTa"].ToString();                     
                        typeacc.Add(acc);
                    }

                    // Thêm khách hàng vào danh sách

                }
                connection.Close();
                reader.Close();

            }
            return typeacc;
        }
    }
}
