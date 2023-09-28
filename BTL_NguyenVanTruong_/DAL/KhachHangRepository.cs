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
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly IConfiguration _configuration;

        public KhachHangRepository(IConfiguration config)
        {
            _configuration = config;
        }

        // Thêm khách hàng
        public bool AddKH(KhachHangModel model)
        {
            try
            {
                // Lấy chuỗi kết nối từ cấu hình
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    SqlCommand command = connection.CreateCommand();
                    // Định nghĩa kiểu của command là 1 thủ tục lưu trữ (không sử dụng câu lệnh sql)
                    command.CommandType = CommandType.StoredProcedure;
                    // Tên của thủ tục lưu trữ
                    command.CommandText = "add_KhachHang";

                    // Định nghĩa các tham số cho thủ tục lưu trữ
                    //Parameters.AddWithValue() : định nghĩa các giá trị tham số và gán giá trị cho chúng.
                    command.Parameters.AddWithValue("@TenKH", model.TenKH);
                    command.Parameters.AddWithValue("@GioiTinh", model.GioiTinh);
                    command.Parameters.AddWithValue("@DiaChi", model.DiaChi);
                    command.Parameters.AddWithValue("@SDT", model.SDT);
                    command.Parameters.AddWithValue("@Email", model.Email);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được thêm vào không
                    return rowsAffected > 0;
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
        public bool UpdateKH(KhachHangModel model)
        {
            return true;
        }

        // Lấy thông tin khách hàng theo id khách hàng
        public KhachHangModel GetDataKHByID(int id)
        {
            // Khởi tạo khachhang
            KhachHangModel khachHang = new KhachHangModel();

            try
            {
                // Lấy chuỗi kết nối csdl
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    SqlCommand command = connection.CreateCommand();
                    // Định nghĩa kiểu của command là 1 thủ tục lưu trữ (không sử dụng câu lệnh sql)
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetKhachHangByID"; // Tên thủ tục lấy thông tin khách hàng

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    command.Parameters.AddWithValue("@Id", id);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = command.ExecuteReader())
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
        public bool DeleteKH(int id)
        {
            try
            {
                // Lấy chuỗi kết nối từ cấu hình
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (var connection = new SqlConnection(connectionString))
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

    }
}

