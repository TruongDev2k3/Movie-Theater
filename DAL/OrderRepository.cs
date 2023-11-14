using DAL.Helper;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using MODEL;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.IO;


namespace DAL
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IConfiguration _configuration;

        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public int AddCustomerAndOrder(OrderDataModel odm)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Thêm thông tin khách hàng vào bảng KhachHangs
                        int customerId = AddCustomer(connection, transaction, odm);

                        // Thêm thông tin hóa đơn vào bảng HoaDons
                        int orderId = AddOrder(connection, transaction, customerId, odm);

                        // Thêm thông tin chi tiết hóa đơn vào bảng ChiTietHoaDons
                        AddOrderDetails(connection, transaction, orderId, odm.ListProduct);

                        transaction.Commit();

                        return customerId; // hoặc trả về orderId nếu cần
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Lỗi server: {ex.Message}");
                    }
                }
            }
        }

        public int AddCustomer(SqlConnection connection, SqlTransaction transaction, OrderDataModel odm)
        {
            string query = "INSERT INTO KhachHangs (TenKH, GioiTinh, DiaChi, SDT, Email) VALUES (@TenKH, @GioiTinh, @DiaChi, @SDT, @Email); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@TenKH", odm.TenKH);
                command.Parameters.AddWithValue("@GioiTinh", odm.GioiTinh);
                command.Parameters.AddWithValue("@DiaChi", odm.DiaChi);
                command.Parameters.AddWithValue("@SDT", odm.SDT);
                command.Parameters.AddWithValue("@Email", odm.Email);

                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public int AddOrder(SqlConnection connection, SqlTransaction transaction, int customerId, OrderDataModel odm)
        {
            string query = "INSERT INTO HoaDons (Id, NgayTao, NgayDuyet, TongGia,TenKH, GioiTinh, DiaChi, SDT, Email) VALUES (@Id, @NgayTao, @NgayDuyet, @TongGia,@TenKH, @GioiTinh, @DiaChi, @SDT, @Email); SELECT SCOPE_IDENTITY();";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@Id", customerId);
                command.Parameters.AddWithValue("@NgayTao", DateTime.Now);
                command.Parameters.AddWithValue("@NgayDuyet", DateTime.Now); // Đặt ngày duyệt là ngày hiện tại
                command.Parameters.AddWithValue("@TongGia", odm.TongGia);
                command.Parameters.AddWithValue("@TenKH", odm.TenKH);
                command.Parameters.AddWithValue("@GioiTinh", odm.GioiTinh);
                command.Parameters.AddWithValue("@DiaChi", odm.DiaChi);
                command.Parameters.AddWithValue("@SDT", odm.SDT);
                command.Parameters.AddWithValue("@Email", odm.Email);
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public void AddOrderDetails(SqlConnection connection, SqlTransaction transaction, int orderId, string listProduct)
        {
            string query = "INSERT INTO ChiTietHoaDons (MaHoaDon, ListProduct) VALUES (@MaHoaDon, @ListProduct);";
            using (SqlCommand command = new SqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@MaHoaDon", orderId);
                command.Parameters.AddWithValue("@ListProduct", listProduct);
                command.ExecuteNonQuery();
            }
        }
    }
}
