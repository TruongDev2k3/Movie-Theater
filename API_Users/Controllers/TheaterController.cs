using BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheaterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ITheaterBusiness _acc;
        public TheaterController(ITheaterBusiness acc)
        {
            _acc = acc;
        }

        [HttpGet("getlist-theater")]
        public ActionResult<List<TheaterModel>> GetTheater()
        {
            try
            {
                var accList = _acc.GetTheater();

                if (accList == null || accList.Count == 0)
                {
                    return NotFound("Danh sách rạp trống");
                }

                return Ok(accList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        [HttpGet("getbyid/{mtl}")]
        public ActionResult<TheaterModel> GetTheaterbyID(int mtl)
        {
            var acc = _acc.GetTheaterbyID(mtl);

            if (acc == null)
            {
                return NotFound();
            }

            return Ok(acc);
        }
        [HttpPost("create-theater")]
        public ActionResult CreateTheater([FromBody] TheaterModel model)
        {
            var result = _acc.CreateTheater(model);
            return Ok(result);
        }
        [HttpPut("update-theater")]
        public ActionResult UpdateTheater([FromBody] TheaterModel model)
        {
            var result = _acc.UpdateTheater(model);
            return Ok(result);
        }
        [HttpDelete("delete-theater/{mtk}")]
        public ActionResult DeleteTheater(int mtk)
        {
            var result = _acc.DeleteTheater(mtk);
            return Ok(result);
        }
    }
}
