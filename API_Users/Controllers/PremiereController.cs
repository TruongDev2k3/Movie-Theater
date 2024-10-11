using BLL.Interfaces;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiereController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IPremiereBusiness _acc;
        public PremiereController(IPremiereBusiness acc)
        {
            _acc = acc;
        }

        [HttpGet("getlistpre")]
        public ActionResult<List<PremiereModel>> GetPremiere()
        {
            try
            {
                var accList = _acc.GetPremiere();

                if (accList == null || accList.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(accList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
    }
}
