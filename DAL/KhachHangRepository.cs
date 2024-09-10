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
    public class KhachHangRepository : IKhachHangRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public KhachHangRepository(IConfiguration configuration)
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
        public List<CustomerModel> GetAllKhachHangs()
        {
            List<CustomerModel> danhSachKhachHang = new List<CustomerModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối

                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetDanhSachKhachHang"; // Tên stored procedure

                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();

                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    CustomerModel khachHang = new CustomerModel();
                    {
                        khachHang.Id = (int)reader["Id"];
                        khachHang.TenKH = reader["TenKH"].ToString();
                        khachHang.GioiTinh = reader["GioiTinh"].ToString();
                        khachHang.DiaChi = reader["DiaChi"].ToString();
                        khachHang.SDT = reader["SDT"].ToString();
                        khachHang.Email = reader["Email"].ToString();
                        //danhSachKhachHang.Add(khachHang);
                    }

                    // Thêm khách hàng vào danh sách.
                    danhSachKhachHang.Add(khachHang);

                }
                connection.Close();
                reader.Close();

            }
            return danhSachKhachHang;
        }


        // Thêm khách hàng
        public bool CreateCustomer(CustomerModel model)
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
                    _command.CommandText = "add_KhachHang";

                    // Định nghĩa các tham số cho thủ tục lưu trữ
                    //Parameters.AddWithValue() : định nghĩa các giá trị tham số và gán giá trị cho chúng.
                    _command.Parameters.AddWithValue("@TenKH", model.TenKH);
                    _command.Parameters.AddWithValue("@GioiTinh", model.GioiTinh);
                    _command.Parameters.AddWithValue("@DiaChi", model.DiaChi);
                    _command.Parameters.AddWithValue("@SDT", model.SDT);
                    _command.Parameters.AddWithValue("@Email", model.Email);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    rowsAffected = _command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được thêm vào không
                    return rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi thêm khách hàng: " + ex.Message);
                return false;
            }
        }



        // Sửa thông tin khách hàng
        public bool UpdateCustomer(CustomerModel model)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    _command = connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "UpdateKhachHang";

                    _command.Parameters.AddWithValue("@Id", model.Id);
                    _command.Parameters.AddWithValue("@TenKH", model.TenKH);
                    _command.Parameters.AddWithValue("@GioiTinh", model.GioiTinh);
                    _command.Parameters.AddWithValue("@DiaChi", model.DiaChi);
                    _command.Parameters.AddWithValue("@SDT", model.SDT);
                    _command.Parameters.AddWithValue("@Email", model.Email);

                    int rowsAffected = _command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật khách hàng: " + ex.Message);
                return false;
            }
        }

        // Lấy thông tin khách hàng theo id khách hàng
        public CustomerModel GetCustomerByID(int id)
        {
            // Khởi tạo khachhang
            CustomerModel khachHang = new CustomerModel();

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
                    _command.CommandText = "GetKhachHangByID"; // Tên thủ tục lấy thông tin khách hàng

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@Id", id);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = _command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            khachHang.Id = (int)reader["Id"];
                            khachHang.TenKH = reader["TenKH"].ToString();
                            khachHang.GioiTinh = reader["GioiTinh"].ToString();
                            khachHang.DiaChi = reader["DiaChi"].ToString();
                            khachHang.SDT = reader["SDT"].ToString();
                            khachHang.Email = reader["Email"].ToString();
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

            return khachHang;
        }

        // Xóa thông tin khách hàng theo id khách hàng
        public bool DeleteCustomer(int id)
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
                    command.CommandText = "delete_kh"; // Thay thế "delete_KhachHang" bằng tên thực tế của thủ tục xóa

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    command.Parameters.AddWithValue("@Id", id);

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

        // Tìm kiếm khách hàng
        public List<CustomerModel> SearchKhachHang(string tukhoa)
        {
            List<CustomerModel> searchResult = new List<CustomerModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                // Create a SqlCommand object to call the stored procedure
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "TimKiemKhachHang"; // Replace with the actual stored procedure name

                    // Add parameter for the stored procedure
                    command.Parameters.AddWithValue("@TuKhoa", tukhoa);

                    // Execute the stored procedure and get the results
                    SqlDataReader reader = command.ExecuteReader();

                    // Read data from the result set
                    while (reader.Read())
                    {
                        CustomerModel khachHang = new CustomerModel();
                        {
                            khachHang.Id = (int)reader["Id"];
                            khachHang.TenKH = reader["TenKH"].ToString();
                            khachHang.GioiTinh = reader["GioiTinh"].ToString();
                            khachHang.DiaChi = reader["DiaChi"].ToString();
                            khachHang.SDT = reader["SDT"].ToString();
                            khachHang.Email = reader["Email"].ToString();
                            searchResult.Add(khachHang);
                        }
                    }

                    reader.Close();
                }

                connection.Close();
            }

            return searchResult;
        }
    }

}


