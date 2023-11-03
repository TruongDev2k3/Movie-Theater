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
    public class ProductRepository : IProductRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public ProductRepository(IConfiguration configuration)
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
        public List<ProductsModel> GetListProduct()
        {
            List<ProductsModel> dssp = new List<ProductsModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối

                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetListProducts"; // Tên stored procedure

                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();

                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    ProductsModel sanPham = new ProductsModel();
                    {
                        sanPham.MaSanPham = (int)reader["MaSanPham"];
                        sanPham.MaChuyenMuc = (int)reader["MaChuyenMuc"];
                        sanPham.TenSanPham = reader["TenSanPham"].ToString();
                        sanPham.AnhSanPham = reader["AnhSanPham"].ToString();
                        sanPham.Gia = (decimal)reader["Gia"];
                        sanPham.GiaGiam = (decimal)reader["GiaGiam"];
                        sanPham.SoLuong = (int)reader["SoLuong"];
                        sanPham.SoLuongDaBan = (int)reader["SoLuongDaBan"];
                        dssp.Add(sanPham);
                    }
                    // Thêm sản phẩm vào danh sách
                }

                connection.Close();
                reader.Close();

            }
            return dssp;
        }


        // Thêm khách hàng
        public bool CreateProduct(ProductsModel model)
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
                    _command.CommandText = "Add_Products";

                    // Định nghĩa các tham số cho thủ tục lưu trữ
                    //Parameters.AddWithValue() : định nghĩa các giá trị tham số và gán giá trị cho chúng.
                    //_command.Parameters.AddWithValue("@MaSanPham", model.MaSanPham);
                    _command.Parameters.AddWithValue("@MaChuyenMuc", model.MaChuyenMuc);
                    _command.Parameters.AddWithValue("@TenSanPham", model.TenSanPham);
                    _command.Parameters.AddWithValue("@AnhSanPham", model.AnhSanPham);
                    _command.Parameters.AddWithValue("@Gia", model.Gia);
                    _command.Parameters.AddWithValue("@GiaGiam", model.GiaGiam);
                    _command.Parameters.AddWithValue("@SoLuong", model.SoLuong);
                    _command.Parameters.AddWithValue("@SoLuongDaBan", model.SoLuongDaBan);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    rowsAffected = _command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được thêm vào không
                    return rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi thêm sản phẩm: " + ex.Message);
                return false;
            }
        }



        // Sửa thông tin khách hàng
        public bool UpdateProduct(ProductsModel model)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    _command = connection.CreateCommand();
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "Update_Products";

                    _command.Parameters.AddWithValue("@MaSanPham", model.MaSanPham);
                    _command.Parameters.AddWithValue("@MaChuyenMuc", model.MaChuyenMuc);
                    _command.Parameters.AddWithValue("@TenSanPham", model.TenSanPham);
                    _command.Parameters.AddWithValue("@AnhSanPham", model.AnhSanPham);
                    _command.Parameters.AddWithValue("@Gia", model.Gia);
                    _command.Parameters.AddWithValue("@GiaGiam", model.GiaGiam);
                    _command.Parameters.AddWithValue("@SoLuong", model.SoLuong);
                    _command.Parameters.AddWithValue("@SoLuongDaBan", model.SoLuongDaBan);


                    int rowsAffected = _command.ExecuteNonQuery();
                    connection.Close();
                    return rowsAffected > 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật sản phẩm: " + ex.Message);
                return false;
            }
        }

        // Lấy thông tin khách hàng theo id khách hàng
        public ProductsModel GetProductByMaSP(int masp)
        {
            // Khởi tạo khachhang
            ProductsModel sp = new ProductsModel();

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
                    _command.CommandText = "GetProductByMaSP"; // Tên thủ tục lấy thông tin khách hàng

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@MaSanPham", masp);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = _command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            sp.MaSanPham = (int)reader["MaSanPham"];
                            sp.MaChuyenMuc = (int)reader["MaChuyenMuc"];
                            sp.TenSanPham = reader["TenSanPham"].ToString();
                            sp.AnhSanPham = reader["AnhSanPham"].ToString();
                            sp.Gia = (decimal)reader["Gia"];
                            sp.GiaGiam = (decimal)reader["GiaGiam"];
                            sp.SoLuong = (int)reader["SoLuong"];
                            sp.SoLuongDaBan = (int)reader["SoLuongDaBan"];
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

            return sp;
        }

        // Xóa thông tin khách hàng theo id khách hàng
        public bool DeleteProduct(int masp)
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
                    command.CommandText = "Delete_Product"; // Thay thế "delete_KhachHang" bằng tên thực tế của thủ tục xóa

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    command.Parameters.AddWithValue("@MaSanPham", masp);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    int rowsAffected = command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã bị xóa không
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi xóa sản phẩm: " + ex.Message);
                return false;
            }
        }

        public List<ProductsModel> SearchProduct(string tensp)
        {
            List<ProductsModel> searchResult = new List<ProductsModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();

                // Create a SqlCommand object to call the stored procedure
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "SearchSanPham"; // Replace with the actual stored procedure name

                    // Add parameter for the stored procedure
                    command.Parameters.AddWithValue("@Keyword", tensp);

                    // Execute the stored procedure and get the results
                    SqlDataReader reader = command.ExecuteReader();

                    // Read data from the result set
                    while (reader.Read())
                    {
                        ProductsModel sanPham = new ProductsModel();
                        {
                            sanPham.MaSanPham = (int)reader["MaSanPham"];
                            sanPham.MaChuyenMuc = (int)reader["MaChuyenMuc"];
                            sanPham.TenSanPham = reader["TenSanPham"].ToString();
                            sanPham.AnhSanPham = reader["AnhSanPham"].ToString();
                            sanPham.Gia = (decimal)reader["Gia"];
                            sanPham.GiaGiam = (decimal)reader["GiaGiam"];
                            sanPham.SoLuong = (int)reader["SoLuong"];
                            sanPham.SoLuongDaBan = (int)reader["SoLuongDaBan"];
                            searchResult.Add(sanPham);
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
