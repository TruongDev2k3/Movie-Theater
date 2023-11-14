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
namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderBusiness _iod;

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
    }
}
