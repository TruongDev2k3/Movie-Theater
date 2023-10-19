using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
namespace BTL_NguyenVanTruong_.API_User.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetListProductController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IProductBusiness _prb;
        public GetListProductController(IProductBusiness prb)
        {
            _prb = prb;
        }

        [HttpGet("getlistoppo")]
        public ActionResult<List<ProductsModel>> GetListOppoProduct()
        {
            try
            {
                var productList = _prb.GetListOppoProduct();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách sản phẩm trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("getlistasus")]
        public ActionResult<List<ProductsModel>> GetListASUSProduct()
        {
            try
            {
                var productList = _prb.GetListASUSProduct();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách sản phẩm trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("getlistlg")]
        public ActionResult<List<ProductsModel>> GetLGTVProduct()
        {
            try
            {
                var productList = _prb.GetLGTVProduct();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách sản phẩm trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("getlistpana")]
        public ActionResult<List<ProductsModel>> GetPanasonicProduct()
        {
            try
            {
                var productList = _prb.GetPanasonicProduct();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách sản phẩm trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("getlistapw")]
        public ActionResult<List<ProductsModel>> GetAppleWatchProduct()
        {
            try
            {
                var productList = _prb.GetAppleWatchProduct();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách sản phẩm trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("getlistdell")]
        public ActionResult<List<ProductsModel>> GetDELLProduct()
        {
            try
            {
                var productList = _prb.GetDELLProduct();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách sản phẩm trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("getlistmsi")]
        public ActionResult<List<ProductsModel>> GetMSIProduct()
        {
            try
            {
                var productList = _prb.GetMSIProduct();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách sản phẩm trống");
                }

                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("getlistss")]
        public ActionResult<List<ProductsModel>> GetSamsungProduct()
        {
            try
            {
                var productList = _prb.GetSamsungProduct();

                if (productList == null || productList.Count == 0)
                {
                    return NotFound("Danh sách sản phẩm sam sung trống");
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
