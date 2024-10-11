using BLL.Interfaces;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DayshowtimeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IDayshowtimeBusiness _acc;
        public DayshowtimeController(IDayshowtimeBusiness acc)
        {
            _acc = acc;
        }

        [HttpGet("getlist-dayshowtime")]
        public ActionResult<List<DayshowModel>> GetDayshowtime()
        {
            try
            {
                var accList = _acc.GetDayshowtime();

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
