using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using MODEL;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoaDonController : ControllerBase
    {
        private readonly IConfiguration _configuration;
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
    }
}
