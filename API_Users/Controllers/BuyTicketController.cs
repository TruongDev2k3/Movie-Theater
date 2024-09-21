using BLL.Interfaces;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuyTicketController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ISeatStatusBussiness _mv;
        private IFoodBusiness _fb;
        public BuyTicketController(ISeatStatusBussiness mv, IFoodBusiness fb)
        {
            _mv = mv;
            _fb = fb;
        }
        [HttpGet("seat/{movieId}/{showDate}/{showTime}")]
        public ActionResult<SeatStatusModel> GetSeatStatus(int movieId, DateTime showDate, [FromRoute] string showTime)
        {
            try
            {
                var showTimeSpan = TimeSpan.Parse(showTime);
                var showtimes = _mv.GetSeatStatus(movieId, showDate, showTimeSpan);
                if (showtimes == null || showtimes.Count == 0)
                {
                    return NotFound("No showtimes found for the specified movie and date.");
                }

                return Ok(showtimes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpGet("get-list-food")]
        public ActionResult<List<FoodModel>> GetFood()
        {
            try
            {
                var foodlist = _fb.GetFood();

                if (foodlist == null || foodlist.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(foodlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        [HttpGet("detailFood/{id}")]
        public ActionResult<FoodModel> GetFoodbyID(int id)
        {
            var accs = _fb.GetFoodbyID(id);

            if (accs == null)
            {
                return NotFound();
            }

            return Ok(accs);
        }

    }
}
