using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using MODEL;
using System.Data.SqlClient;
using System.Data;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        public static IConfiguration _configuration { get; set; }

        private IHoaDonBusiness _hdb;
        

        public HoaDonController(IHoaDonBusiness hdb)
        {
            _hdb = hdb;
        }
        [HttpGet("getlisthd")]
        public ActionResult<List<HoaDonModel>> GetHoaDon()
        {
            try
            {
                var listhd = _hdb.GetHoaDon();

                if (listhd == null || listhd.Count == 0)
                {
                    return NotFound("Danh sách hóa đơn trống");
                }

                return Ok(listhd);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }


        [HttpGet("getbyid/{id}")]
        public ActionResult<HoaDonModel> GetHoaDonbyID(int id)
        {
            var product = _hdb.GetHoaDonbyID(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("create-hd")]
        public ActionResult CreateHoaDon([FromBody] HoaDonModel model)
        {
            var result = _hdb.CreateHoaDon(model);
            return Ok(result);
        }

        [HttpPut("update-hd")]
        public ActionResult UpdateHoaDon([FromBody] HoaDonModel model)
        {
            var result = _hdb.UpdateHoaDon(model);
            return Ok(result);
        }

        [HttpDelete("delete-hoadon/{mahd}")]
        public ActionResult DeleteProduct(int mahd)
        {
            var result = _hdb.DeleteHoaDon(mahd);
            return Ok(result);
        }
        [HttpPost("searchhd")]
        public ActionResult<List<HoaDonModel>> SearchHoaDon(string tenkh)
        {
            try
            {
                var productList = _hdb.SearchHoaDon(tenkh);

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách hóa đơn trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }
        [HttpPut("update-order-status/{maHoaDon}")]
        public IActionResult UpdateOrderStatus(int maHoaDon)
        {
            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandText = @"
                
                    UPDATE HoaDons
                    SET TrangThai = N'Đã xác nhận', NgayDuyet = GETDATE()
                    WHERE MaHoaDon = @MaHoaDon";

                    command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { message = "Cập nhật trạng thái thành công" });
                    }
                    else
                    {
                        return NotFound(new { message = "Không tìm thấy mã hóa đơn" });
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
