using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DayshowtimeRepository : IDayshowtimeRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public DayshowtimeRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public List<DayshowModel> GetDayshowtime()
        {
            List<DayshowModel> acc = new List<DayshowModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetDayshowWithShowtime"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    DayshowModel ac = new DayshowModel();
                    {
                        ac.DayShowId = (int)reader["dayShowId"];                      
                        ac.DayShowtime = (DateTime)reader["day_showtime"];           
                        ac.PremiereId = reader["showtime"].ToString();              
                        ac.day_of_week = reader["day_of_week"].ToString();
                        acc.Add(ac);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return acc;
        }



        //public bool CreateAccount(AccountModel model)
        //{

        //    try
        //    {
        //        int rowsAffected = 0;
        //        // Lấy chuỗi kết nối từ cấu hình
        //        using (var connection = new SqlConnection(GetConnectionString()))
        //        {
        //            connection.Open();

        //            // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
        //            _command = connection.CreateCommand();
        //            // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
        //            _command.CommandType = CommandType.StoredProcedure;
        //            // Tên của thủ tục lưu trữ
        //            _command.CommandText = "ThemTaiKhoan";

        //            // Định nghĩa các tham số cho thủ tục lưu trữ
        //            //Parameters.AddWithValue() : định nghĩa các giá trị tham số và gán giá trị cho chúng.
        //            _command.Parameters.AddWithValue("@MaLoai", model.MaLoai);
        //            _command.Parameters.AddWithValue("@TenTaiKhoan", model.TenTaiKhoan);
        //            _command.Parameters.AddWithValue("@MatKhau", model.MatKhau);
        //            _command.Parameters.AddWithValue("@Email", model.Email);
        //            _command.Parameters.AddWithValue("@Loai", model.Loai);

        //            // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
        //            rowsAffected = _command.ExecuteNonQuery();

        //            // Kiểm tra xem có bản ghi nào đã được thêm vào không
        //            return rowsAffected > 0 ? true : false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
        //        Console.WriteLine("Lỗi khi thêm khách hàng: " + ex.Message);
        //        return false;
        //    }
        //}



        //// Sửa thông tin khách hàng
        //public bool UpdateAccount(AccountModel model)
        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(GetConnectionString()))
        //        {
        //            connection.Open();

        //            _command = connection.CreateCommand();
        //            _command.CommandType = CommandType.StoredProcedure;
        //            _command.CommandText = "SuaTaiKhoan";
        //            _command.Parameters.AddWithValue("@MaTaiKhoan", model.MaTaiKhoan);
        //            _command.Parameters.AddWithValue("@MaLoai", model.MaLoai);
        //            _command.Parameters.AddWithValue("@TenTaiKhoan", model.TenTaiKhoan);
        //            _command.Parameters.AddWithValue("@MatKhau", model.MatKhau);
        //            _command.Parameters.AddWithValue("@Email", model.Email);
        //            _command.Parameters.AddWithValue("@Loai", model.Loai);

        //            int rowsAffected = _command.ExecuteNonQuery();
        //            connection.Close();
        //            return rowsAffected > 0;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Lỗi khi cập nhật tài khoản: " + ex.Message);
        //        return false;
        //    }
        //}

        //// Lấy thông tin khách hàng theo id khách hàng


        //// Xóa thông tin khách hàng theo id khách hàng
        //public bool DeleteAccount(int mtk)
        //{
        //    try
        //    {
        //        // Lấy chuỗi kết nối từ cấu hình
        //        using (var connection = new SqlConnection(GetConnectionString()))
        //        {
        //            connection.Open();

        //            // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
        //            SqlCommand command = connection.CreateCommand();
        //            // Định nghĩa kiểu của command là 1 thủ tục lưu trữ (không sử dụng câu lệnh SQL)
        //            command.CommandType = CommandType.StoredProcedure;
        //            // Tên của thủ tục lưu trữ xóa khách hàng
        //            command.CommandText = "XoaTaiKhoan"; // Thay thế "delete_KhachHang" bằng tên thực tế của thủ tục xóa

        //            // Định nghĩa tham số cho thủ tục lưu trữ
        //            command.Parameters.AddWithValue("@MaTaiKhoan", mtk);

        //            // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
        //            int rowsAffected = command.ExecuteNonQuery();

        //            // Kiểm tra xem có bản ghi nào đã bị xóa không
        //            return rowsAffected > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
        //        Console.WriteLine("Lỗi khi xóa khách hàng: " + ex.Message);
        //        return false;
        //    }
        //}
    }
}
