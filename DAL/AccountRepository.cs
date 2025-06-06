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
    public class AccountRepository : IAccountRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public AccountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public List<AccountModel> GetAccount()
        {
            List<AccountModel> acc = new List<AccountModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetAllTaiKhoans"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    AccountModel ac = new AccountModel();
                    {
                        ac.MaTaiKhoan = (int)reader["MaTaiKhoan"];
                        ac.LoaiTaiKhoan = (int)reader["LoaiTaiKhoan"];
                        ac.TenTaiKhoan = reader["TenTaiKhoan"].ToString();
                        ac.MatKhau = reader["MatKhau"].ToString();
                        ac.Email = reader["Email"].ToString();
                        ac.Loai = reader["Loai"].ToString();
                        ac.nameUsser = reader["nameUsser"].ToString();
                        acc.Add(ac);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return acc;
        }



        public bool CreateAccount(AccountModel model, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "InsertTaiKhoan";

                        cmd.Parameters.AddWithValue("@LoaiTaiKhoan", model.LoaiTaiKhoan);
                        cmd.Parameters.AddWithValue("@TenTaiKhoan", model.TenTaiKhoan);
                        cmd.Parameters.AddWithValue("@MatKhau", model.MatKhau);
                        cmd.Parameters.AddWithValue("@Email", model.Email);
                        cmd.Parameters.AddWithValue("@Loai", model.Loai);
                        cmd.Parameters.AddWithValue("@nameUsser", model.nameUsser);

                        // Tham số output để nhận lỗi
                        var paramError = new SqlParameter("@ErrorMessage", SqlDbType.NVarChar, 250);
                        paramError.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(paramError);

                        // Thực thi
                        var returnValue = cmd.ExecuteNonQuery();

                        // Lấy thông báo lỗi trả về
                        errorMessage = paramError.Value as string;

                        if (!string.IsNullOrEmpty(errorMessage))
                        {
                            return false; // Có lỗi trùng, không tạo được tài khoản
                        }

                        return true; // Thành công
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi khi thêm tài khoản: " + ex.Message;
                return false;
            }
        }


        // Sửa thông tin khách hàng
        public bool UpdateAccount(AccountModel model)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    _command = connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "UpdateTaiKhoan";
                    _command.Parameters.AddWithValue("@MaTaiKhoan", model.MaTaiKhoan);
                    _command.Parameters.AddWithValue("@LoaiTaiKhoan", model.LoaiTaiKhoan);
                    _command.Parameters.AddWithValue("@TenTaiKhoan", model.TenTaiKhoan);
                    _command.Parameters.AddWithValue("@MatKhau", model.MatKhau);
                    _command.Parameters.AddWithValue("@Email", model.Email);
                    _command.Parameters.AddWithValue("@Loai", model.Loai);
                    _command.Parameters.AddWithValue("@nameUsser", model.nameUsser);

                    int rowsAffected = _command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật tài khoản: " + ex.Message);
                return false;
            }
        }

        // Lấy thông tin khách hàng theo id khách hàng
       

        // Xóa thông tin khách hàng theo id khách hàng
        public bool DeleteAccount(int mtk)
        {
            try
            {
                // Lấy chuỗi kết nối từ cấu hình
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    SqlCommand command = connection.CreateCommand();
                    // Định nghĩa kiểu của command là 1 thủ tục lưu trữ (không sử dụng câu lệnh SQL)
                    command.CommandType = CommandType.StoredProcedure;
                    // Tên của thủ tục lưu trữ xóa khách hàng
                    command.CommandText = "DeleteTaiKhoan"; // Thay thế "delete_KhachHang" bằng tên thực tế của thủ tục xóa

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    command.Parameters.AddWithValue("@MaTaiKhoan", mtk);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã bị xóa không
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi xóa khách hàng: " + ex.Message);
                return false;
            }
        }
        public AccountModel GetAccountbyID(int mtk)
        {
            // Khởi tạo khachhang
            AccountModel ac = new AccountModel();

            try
            {
                // Lấy chuỗi kết nối csdl
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    _command = connection.CreateCommand();
                    // Định nghĩa kiểu của command là 1 thủ tục lưu trữ (không sử dụng câu lệnh sql)
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "GetTaiKhoanById"; // Tên thủ tục lấy thông tin khách hàng

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@MaTaiKhoan", mtk);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = _command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            ac.MaTaiKhoan = (int)reader["MaTaiKhoan"];
                            ac.LoaiTaiKhoan = (int)reader["LoaiTaiKhoan"];
                            ac.TenTaiKhoan = reader["TenTaiKhoan"].ToString();
                            ac.MatKhau = reader["MatKhau"].ToString();
                            ac.Email = reader["Email"].ToString();
                            ac.Loai = reader["Loai"].ToString();
                            ac.nameUsser = reader["nameUsser"].ToString();
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi lấy thông tin khách hàng: " + ex.Message);
            }

            return ac;
        }
        // Tìm kiếm khách hàng
        
    }
}
