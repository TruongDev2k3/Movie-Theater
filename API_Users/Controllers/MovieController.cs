using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using BLL;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieBusiness _mv;
        private ITools _tools;

        public MovieController(IMovieBusiness mv,ITools tools)
        {
            _mv = mv;
            _tools = tools;
        }
        [Route("upload")]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {
                    string filePath = $"/img/{file.FileName.Replace("-", "_").Replace("%", "")}";
                    var fullPath = _tools.CreatePathFile(filePath);
                    using (var fileStream = new FileStream(fullPath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(new { filePath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Không thể upload tệp");
            }
        }
        [HttpGet("get-list-movie")]
        public ActionResult<List<MovieModel>> GetMovie()
        {
            try
            {
                var mvlist = _mv.GetMovie();

                if (mvlist == null || mvlist.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(mvlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("get-top-movie")]
        public ActionResult<List<MovieModel>> GetTopMovie()
        {
            try
            {
                var mvlist = _mv.GetTopMovie();

                if (mvlist == null || mvlist.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(mvlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("get-nowshowing-movie")]
        public ActionResult<List<MovieModel>> GetMoviesNowShowing()
        {
            try
            {
                var mvlist = _mv.GetMoviesNowShowing();

                if (mvlist == null || mvlist.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(mvlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        [HttpGet("get-upcoming-movie")]
        public ActionResult<List<MovieModel>> GetUpcomingMovies()
        {
            try
            {
                var mvlist = _mv.GetUpcomingMovies();

                if (mvlist == null || mvlist.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(mvlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        [HttpGet("get-proceeds-movie")]
        public ActionResult<List<DoanhThuModel>> GetProceeds()
        {
            try
            {
                var mvlist = _mv.GetProceeds();

                if (mvlist == null || mvlist.Count == 0)
                {
                    return NotFound("Danh sách khách hàng trống");
                }

                return Ok(mvlist);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }
        [HttpGet("detailMovie/{id}")]
        public ActionResult<MovieModel> GetMovieById(int id)
        {
            var acc = _mv.GetMovieById(id);

            if (acc == null)
            {
                return NotFound();
            }

            return Ok(acc);
        }
        [HttpGet("trailer/{id}")]
        public ActionResult<MovieModel> GetTrailerbyID(int id)
        {
            var accs = _mv.GetTrailerById(id);

            if (accs == null)
            {
                return NotFound();
            }

            return Ok(accs);
        }
        [HttpGet("showtimes/{date}")]
        public ActionResult<FilmAndShowTimeModel> GetShowtimesByDate(string date)
        {
            var showtimes = _mv.GetShowtimesByDate(date);
            return Ok(showtimes);
        }

        [HttpGet("get-showday/{movieId}")]
        public ActionResult<DayshowModel> GetMovieShowDays(int movieId)
        {
            var date = _mv.GetMovieShowDays(movieId);
            return Ok(date);
        }
        [HttpGet("get-showtimes/{movieId}/{dayShowtime}")]
        public ActionResult<PremiereModel> GetShowtimesByMovieAndDate(int movieId, DateTime dayShowtime)
        {
            try
            {
                var showtimes = _mv.GetShowtimesByMovieAndDate(movieId, dayShowtime);
                if (showtimes == null || showtimes.Count == 0)
                {
                    return NotFound("No showtimes found for the specified movie and date.");
                }

                return Ok(showtimes);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi và trả về mã lỗi 500 nếu có lỗi xảy ra
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
        [HttpPost("order-ticket")]
        public ActionResult OrderTicket([FromBody] TicketModel model)
        {
            var result = _mv.OrderTicket(model);
            return Ok(result);
        }

        [HttpPost("create-movie")]
        public ActionResult CreateMovie([FromBody] MovieModel model)
        {
            var result = _mv.CreateMovie(model);
            return Ok(result);
        }

        [HttpPut("update-movie")]
        public ActionResult UpdateMovie([FromBody] MovieModel model)
        {
            var result = _mv.UpdateMovie(model);
            return Ok(result);
        }

        [HttpDelete("delete-movie/{movieId}")]
        public ActionResult DeleteMovie(int movieId)
        {
            var result = _mv.DeleteMovie(movieId);
            return Ok(result);
        }
    }
}
