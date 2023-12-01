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
    public class QuangCaoController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IQuangCaoBusiness _khb;
        public QuangCaoController(IQuangCaoBusiness khb)
        {
            _khb = khb;
        }
        [HttpGet("getlistQC")]

        public ActionResult<List<QuangCaoModel>> GetQuangCao()
        {
            try
            {
                var QCList = _khb.GetQuangCao();

                if (QCList == null || QCList.Count == 0)
                {
                    return NotFound("Danh sách quảng cáo trống");
                }

                return Ok(QCList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }



        [HttpGet("getbyid/{id}")]
        public ActionResult<QuangCaoModel> GetQCbyID(int id)
        {
            var qc = _khb.GetQCbyID(id);

            if (qc == null)
            {
                return NotFound();
            }

            return Ok(qc);
        }

        [HttpPost("create-qc")]
        public ActionResult CreateQuangCao([FromBody] QuangCaoModel model)
        {
            var result = _khb.CreateQuangCao(model);
            return Ok(result);
        }

        [HttpPut("update-qc")]
        public ActionResult UpdateQuangCao([FromBody] QuangCaoModel model)
        {
            var result = _khb.UpdateQuangCao(model);
            return Ok(result);
        }

        [HttpDelete("delete-qc/{maqc}")]
        public ActionResult DeleteQuangCao(int maqc)
        {
            var result = _khb.DeleteQuangCao(maqc);
            return Ok(result);
        }
        
    }
}
