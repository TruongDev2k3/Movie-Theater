using Microsoft.AspNetCore.Mvc;
using MODEL;
using Microsoft.Extensions.Configuration;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using System.Data;
namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _iod;
        public static IConfiguration _configuration { get; set; }
        public OrderController(IOrderBusiness iod)
        {
            _iod = iod;
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderDataModel orderData)
        {
            try
            {
                // Thêm thông tin khách hàng và hóa đơn
                int customerId = _iod.AddCustomerAndOrder(orderData);

                // Thêm thông tin chi tiết hóa đơn


                return Ok(new { Message = "Đặt hàng thành công!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("order-details/{maHoaDon}")]
        public IActionResult GetOrderDetails(int maHoaDon)
        {
            List<OrderDetailModel> orderDetails = new List<OrderDetailModel>();

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
            SELECT 
                MaChiTietHoaDon, 
                MaHoaDon, 
                JSON_VALUE(value, '$.maSanPham') AS id,
                JSON_VALUE(value, '$.anhSanPham') AS anhSanPham,
                JSON_VALUE(value, '$.tenSanPham') AS tenSanPham,
                JSON_VALUE(value, '$.giaGiam') AS giaGiam,
                JSON_VALUE(value, '$.soluong') AS soluong
            FROM 
                ChiTietHoaDons
            CROSS APPLY 
                OPENJSON(ListProduct)
            WHERE 
                MaHoaDon = @MaHoaDon";

                    command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        var maChiTietHoaDon = reader["MaChiTietHoaDon"];
                        var maHoaDonDb = reader["MaHoaDon"];
                        var id = reader["id"];
                        var anhSanPham = reader["anhSanPham"];
                        var tenSanPham = reader["tenSanPham"];
                        var giaGiam = reader["giaGiam"];
                        var soluong = reader["soluong"];

                        OrderDetailModel orderDetail = new OrderDetailModel
                        {
                            MaChiTietHoaDon = maChiTietHoaDon != DBNull.Value ? Convert.ToInt32(maChiTietHoaDon) : 0,
                            MaHoaDon = maHoaDonDb != DBNull.Value ? Convert.ToInt32(maHoaDonDb) : 0,
                            Id = id != DBNull.Value ? Convert.ToInt32(id) : 0,
                            AnhSanPham = anhSanPham?.ToString() ?? string.Empty,
                            TenSanPham = tenSanPham?.ToString() ?? string.Empty,
                            GiaGiam = giaGiam != DBNull.Value ? Convert.ToDecimal(giaGiam) : 0,
                            SoLuong = soluong != DBNull.Value ? Convert.ToInt32(soluong) : 0
                        };

                        orderDetails.Add(orderDetail);
                    }

                    reader.Close();
                }

                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        public class OrderDetailModel
        {
            public int MaChiTietHoaDon { get; set; }
            public int MaHoaDon { get; set; }
            public int Id { get; set; }
            public string AnhSanPham { get; set; }
            public string TenSanPham { get; set; }
            public decimal GiaGiam { get; set; }
            public int SoLuong { get; set; }
        }

    }
}
