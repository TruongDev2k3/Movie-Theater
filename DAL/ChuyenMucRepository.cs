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
    public class ChuyenMucRepository : IChuyenMucRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public ChuyenMucRepository(IConfiguration configuration)
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
        public List<ChuyenMucModel> GetChuyenMuc()
        {
            List<ChuyenMucModel> dsachcm = new List<ChuyenMucModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối

                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetChuyenMuc"; // Tên stored procedure

                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();

                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    ChuyenMucModel cm = new ChuyenMucModel();
                    {
                        cm.MaChuyenMuc = (int)reader["MaChuyenMuc"];
                        cm.MaChuyenMucCha = (int)reader["MaChuyenMucCha"];
                        cm.TenChuyenMuc = reader["TenChuyenMuc"].ToString();
                        cm.NoiDung = reader["NoiDung"].ToString();
                        dsachcm.Add(cm);
                    }

                    // Thêm khách hàng vào danh sách

                }
                connection.Close();
                reader.Close();

            }
            return dsachcm;
        }


        // Thêm khách hàng
        public bool CreateChuyenMuc(ChuyenMucModel model)
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
                    _command.CommandText = "AddChuyenMuc";

                    // Định nghĩa các tham số cho thủ tục lưu trữ
                    //Parameters.AddWithValue() : định nghĩa các giá trị tham số và gán giá trị cho chúng.
                    _command.Parameters.AddWithValue("@MaChuyenMucCha", model.MaChuyenMucCha);
                    _command.Parameters.AddWithValue("@TenChuyenMuc", model.TenChuyenMuc);
                    _command.Parameters.AddWithValue("@NoiDung", model.NoiDung);
                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    rowsAffected = _command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được thêm vào không
                    return rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi thêm chuyên mục: " + ex.Message);
                return false;
            }
        }



        // Sửa thông tin khách hàng
        public bool UpdateChuyenMuc(ChuyenMucModel model)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    _command = connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "UpdateChuyenMuc";
                    _command.Parameters.AddWithValue("@MaChuyenMuc", model.MaChuyenMuc);
                    _command.Parameters.AddWithValue("@MaChuyenMucCha", model.MaChuyenMucCha);
                    _command.Parameters.AddWithValue("@TenChuyenMuc", model.TenChuyenMuc);
                    _command.Parameters.AddWithValue("@NoiDung", model.NoiDung);

                    int rowsAffected = _command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật chuyên mục: " + ex.Message);
                return false;
            }
        }

        // Lấy thông tin khách hàng theo id khách hàng
        public ChuyenMucModel GetCMbyID(int mcm)
        {
            // Khởi tạo khachhang
            ChuyenMucModel cm = new ChuyenMucModel();

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
                    _command.CommandText = "GetChuyenMucByMCM"; // Tên thủ tục lấy thông tin khách hàng

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@MaChuyenMuc", mcm);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = _command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            cm.MaChuyenMuc = (int)reader["MaChuyenMuc"];
                            cm.MaChuyenMucCha = (int)reader["MaChuyenMucCha"];
                            cm.TenChuyenMuc = reader["TenChuyenMuc"].ToString();
                            cm.NoiDung = reader["NoiDung"].ToString();
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

            return cm;
        }

        // Xóa thông tin khách hàng theo id khách hàng
        public bool DeleteChuyenMuc(int mcm)
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
                    command.CommandText = "DeleteChuyenMuc"; // Thay thế "delete_KhachHang" bằng tên thực tế của thủ tục xóa

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    command.Parameters.AddWithValue("@MaChuyenMuc", mcm);

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

        public List<ChuyenMucModel> SearchChuyenMuc(string tencm)
        {
            List<ChuyenMucModel> dsachcm = new List<ChuyenMucModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SearchChuyenMuc"; // Thay "SearchChuyenMuc" bằng tên stored procedure tương ứng

                    // Thêm tham số đầu vào cho stored procedure
                    command.Parameters.AddWithValue("@Keyword", tencm);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        ChuyenMucModel cm = new ChuyenMucModel();
                        {
                            cm.MaChuyenMuc = (int)reader["MaChuyenMuc"];
                            cm.MaChuyenMucCha = (int)reader["MaChuyenMucCha"];
                            cm.TenChuyenMuc = reader["TenChuyenMuc"].ToString();
                            cm.NoiDung = reader["NoiDung"].ToString();
                            dsachcm.Add(cm);
                        }
                    }

                    reader.Close();
                }

                connection.Close();
            }

            return dsachcm;
        }


    }
}
