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
    public class HoaDonRepository : IHoaDonRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public HoaDonRepository(IConfiguration configuration)
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
        public List<HoaDonModel> GetHoaDon()
        {
            List<HoaDonModel> dshoadon = new List<HoaDonModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối

                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetHoaDon"; // Tên stored procedure

                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();

                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    HoaDonModel hoadon = new HoaDonModel();
                    {
                        hoadon.MaHoaDon = (int)reader["MaHoaDon"];
                        hoadon.NgayTao = (DateTime)reader["NgayTao"];
                        hoadon.NgayDuyet = (DateTime)reader["NgayTao"];
                        hoadon.TongGia = (decimal)reader["TongGia"];
                        hoadon.Id = (int)reader["Id"];
                        hoadon.TenKH = reader["TenKH"].ToString();
                        hoadon.GioiTinh = reader["GioiTinh"].ToString();
                        hoadon.DiaChi = reader["DiaChi"].ToString();
                        hoadon.Email = reader["Email"].ToString();
                        hoadon.SDT = reader["SDT"].ToString();
                        dshoadon.Add(hoadon);
                    }

                    // Thêm khách hàng vào danh sách

                }
                connection.Close();
                reader.Close();

            }
            return dshoadon;
        }


        // Thêm khách hàng
        public bool CreateHoaDon(HoaDonModel model)
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
                    _command.CommandText = "InsertHoaDon";

                    // Định nghĩa các tham số cho thủ tục lưu trữ
                    //Parameters.AddWithValue() : định nghĩa các giá trị tham số và gán giá trị cho chúng.
                    
                    _command.Parameters.AddWithValue("@NgayTao", model.NgayTao);
                    _command.Parameters.AddWithValue("@NgayDuyet", model.NgayDuyet); // Đặt ngày duyệt là ngày hiện tại
                    _command.Parameters.AddWithValue("@TongGia", model.TongGia);
                    _command.Parameters.AddWithValue("@Id", model.Id);
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
        public bool UpdateHoaDon(HoaDonModel model)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    _command = connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "UpdateHoaDon";

                    _command.Parameters.AddWithValue("@MaHoaDon", model.MaHoaDon);
                    _command.Parameters.AddWithValue("@NgayTao", model.NgayTao);
                    _command.Parameters.AddWithValue("@NgayDuyet", model.NgayDuyet); // Đặt ngày duyệt là ngày hiện tại
                    _command.Parameters.AddWithValue("@TongGia", model.TongGia);
                    _command.Parameters.AddWithValue("@TenKH", model.TenKH);
                    _command.Parameters.AddWithValue("@GioiTinh", model.GioiTinh);
                    _command.Parameters.AddWithValue("@DiaChi", model.DiaChi);
                    _command.Parameters.AddWithValue("@Email", model.Email);
                    _command.Parameters.AddWithValue("@SDT", model.SDT);
                    

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
        public HoaDonModel GetHoaDonbyID(int mhd)
        {
            // Khởi tạo khachhang
            HoaDonModel hoadon = new HoaDonModel();

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
                    _command.CommandText = "GetByMHD"; // Tên thủ tục lấy thông tin khách hàng

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@MaHoaDon", mhd);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = _command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            hoadon.MaHoaDon = (int)reader["MaHoaDon"];
                            hoadon.NgayTao = (DateTime)reader["NgayTao"];
                            hoadon.NgayDuyet = (DateTime)reader["NgayTao"];
                            hoadon.TongGia = (decimal)reader["TongGia"];
                            hoadon.Id = (int)reader["Id"];
                            hoadon.TenKH = reader["TenKH"].ToString();
                            hoadon.GioiTinh = reader["GioiTinh"].ToString();
                            hoadon.DiaChi = reader["DiaChi"].ToString();
                            hoadon.Email = reader["Email"].ToString();
                            hoadon.SDT = reader["SDT"].ToString();                         
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

            return hoadon;
        }

        // Xóa thông tin khách hàng theo id khách hàng
        public bool DeleteHoaDon(int mhd)
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
                    command.CommandText = "DeleteHoaDon"; // Thay thế "delete_KhachHang" bằng tên thực tế của thủ tục xóa

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    command.Parameters.AddWithValue("@MaHoaDon", mhd);

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
        public List<HoaDonModel> SearchHoaDon(string tenkh)
        {
            List<HoaDonModel> searchResult = new List<HoaDonModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                // Create a SqlCommand object to call the stored procedure
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SearchHD"; // Replace with the actual stored procedure name

                    // Add parameter for the stored procedure
                    command.Parameters.AddWithValue("@TuKhoa", tenkh);

                    // Execute the stored procedure and get the results
                    SqlDataReader reader = command.ExecuteReader();

                    // Read data from the result set
                    while (reader.Read())
                    {
                        HoaDonModel hoadon = new HoaDonModel();
                        {
                            hoadon.MaHoaDon = (int)reader["MaHoaDon"];
                            hoadon.NgayTao = (DateTime)reader["NgayTao"];
                            hoadon.NgayDuyet = (DateTime)reader["NgayTao"];
                            hoadon.TongGia = (decimal)reader["TongGia"];
                            hoadon.Id = (int)reader["Id"];
                            hoadon.TenKH = reader["TenKH"].ToString();
                            hoadon.GioiTinh = reader["GioiTinh"].ToString();
                            hoadon.DiaChi = reader["DiaChi"].ToString();
                            hoadon.Email = reader["Email"].ToString();
                            hoadon.SDT = reader["SDT"].ToString();
                            searchResult.Add(hoadon);
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
