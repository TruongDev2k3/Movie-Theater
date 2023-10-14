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
    public class ChuyenMucController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IChuyenMucBusiness _prb;
        public ChuyenMucController(IChuyenMucBusiness prb)
        {
            _prb = prb;
        }
        [HttpGet("getlistCM")]
        public ActionResult<List<ChuyenMucModel>> GetChuyenMuc()
        {
            try
            {
                var cmlist = _prb.GetChuyenMuc();

                if (cmlist == null || cmlist.Count == 0)
                {
                    return NotFound("Danh sách chuyên mục trống");
                }

                return Ok(cmlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        
      
        [HttpGet("getbyid/{mcm}")]
        public ActionResult<ChuyenMucModel> GetCMbyID(int mcm)
        {
            var cm = _prb.GetCMbyID(mcm);

            if (cm == null)
            {
                return NotFound();
            }

            return Ok(cm);
        }

        [HttpPost("create-cm")]
        public ActionResult CreateChuyenMuc([FromBody] ChuyenMucModel model)
        {
            var result = _prb.CreateChuyenMuc(model);
            return Ok(result);
        }

        [HttpPut("update-cm")]
        public ActionResult UpdateChuyenMuc([FromBody] ChuyenMucModel model)
        {
            var result = _prb.UpdateChuyenMuc(model);
            return Ok(result);
        }

        [HttpDelete("delete-cm/{mcm}")]
        public ActionResult DeleteChuyenMuc(int mcm)
        {
            var result = _prb.DeleteChuyenMuc(mcm);
            return Ok(result);
        }
    }
}
