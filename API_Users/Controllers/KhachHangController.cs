using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using MODEL;
using Microsoft.AspNetCore.Authorization;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class KhachHangController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IKhachHangBusiness _khb;
        public KhachHangController(IKhachHangBusiness khb)
        {
            _khb = khb;
        }
        [HttpGet("getlistkh")]
        
        public ActionResult<List<CustomerModel>> GetAllKhachHangs() 
        {
            try
            {
                var productList = _khb.GetAllKhachHangs();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }



        [HttpGet("getbyid/{id}")]
        public ActionResult<CustomerModel> GetCustomerByID(int id)
        {
            var product = _khb.GetCustomerByID(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost("create-kh")]
        public ActionResult CreateProduct([FromBody] CustomerModel model)
        {
            var result = _khb.CreateCustomer(model);
            return Ok(result);
        }

        [HttpPut("update-kh")]
        public ActionResult UpdateProduct([FromBody] CustomerModel model)
        {
            var result = _khb.UpdateCustomer(model);
            return Ok(result);
        }

        [HttpDelete("delete-kh/{makh}")]
        public ActionResult DeleteProduct(int makh)
        {
            var result = _khb.DeleteCustomer(makh);
            return Ok(result);
        }
        [HttpPost("searchkh")]
        public ActionResult<List<CustomerModel>> SearchProduct(string tukhoa)
        {
            try
            {
                var productList = _khb.SearchKhachHang(tukhoa);

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
