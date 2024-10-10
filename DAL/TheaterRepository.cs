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
    public class TheaterRepository : ITheaterRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public TheaterRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public List<TheaterModel> GetTheater()
        {
            List<TheaterModel> ctg = new List<TheaterModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetAllTheaters"; // Tên stored procedure
                                                              // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    TheaterModel tl = new TheaterModel();
                    {
                        tl.TheaterId = (int)reader["theaterId"];
                        tl.TheaterName = reader["theaterName"].ToString();
                        tl.Description = reader["description"].ToString();
                        tl.ImageTheater = reader["imageTheater"].ToString();
                        ctg.Add(tl);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return ctg;
        }



        public bool CreateTheater(TheaterModel model)
        {

            try
            {
                int rowsAffected = 0;
                // Lấy chuỗi kết nối từ cấu hình
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    _command = connection.CreateCommand();
                    // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                    _command.CommandType = CommandType.StoredProcedure;
                    // Tên của thủ tục lưu trữ
                    _command.CommandText = "CreateTheater";

                    // Định nghĩa các tham số cho thủ tục lưu trữ
                    //Parameters.AddWithValue() : định nghĩa các giá trị tham số và gán giá trị cho chúng.
                    _command.Parameters.AddWithValue("@theaterName", model.TheaterName);
                    _command.Parameters.AddWithValue("@description", model.Description);
                    _command.Parameters.AddWithValue("@imageTheater", model.ImageTheater);                                  

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    rowsAffected = _command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được thêm vào không
                    return rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi thêm thể loại: " + ex.Message);
                return false;
            }
        }



        // Sửa thông tin thể loại
        public bool UpdateTheater(TheaterModel model)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    _command = connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "UpdateTheater";
                    _command.Parameters.AddWithValue("@theaterId", model.TheaterId);
                    _command.Parameters.AddWithValue("@theaterName", model.TheaterName);
                    _command.Parameters.AddWithValue("@description", model.Description);
                    _command.Parameters.AddWithValue("@imageTheater", model.ImageTheater);

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

        // Lấy thông tin thể loại theo id thể loại


        // Xóa thông tin thể loại theo id thể loại
        public bool DeleteTheater(int mtl)
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
                    // Tên của thủ tục lưu trữ xóa thể loại
                    command.CommandText = "DeleteTheater"; // Thay thế "delete_KhachHang" bằng tên thực tế của thủ tục xóa

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    command.Parameters.AddWithValue("@theaterId", mtl);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã bị xóa không
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi xóa thể loại: " + ex.Message);
                return false;
            }
        }
        public TheaterModel GetTheaterbyID(int mtl)
        {
            // Khởi tạo khachhang
            TheaterModel tl = new TheaterModel();

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
                    _command.CommandText = "GetTheaterById"; // Tên thủ tục lấy thông tin thể loại

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@theaterId", mtl);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = _command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tl.TheaterId = (int)reader["theaterId"];
                            tl.TheaterName = reader["theaterName"].ToString();
                            tl.Description = reader["description"].ToString();
                            tl.ImageTheater = reader["imageTheater"].ToString();

                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi lấy thông tin thể loại: " + ex.Message);
            }

            return tl;
        }

    }
}
   


