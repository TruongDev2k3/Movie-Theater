using BLL.Interfaces;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;
using System.Net.Mail;
using QRCoder;
using System.Drawing;
using System.IO;
using MailKit.Net.Smtp;
using MimeKit;
using System.Net;
using System.Net.Mail;
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
                    return NotFound("Danh sách đồ ăn hàng trống");
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

        [HttpPut("update-seatstatus")]
        public IActionResult UpdateSeatStatus([FromBody] UpdateSeatAdmin model)
        {
            if (model == null || string.IsNullOrEmpty(model.SeatNames))
            {
                return BadRequest("Invalid input data.");
            }

            bool result = _mv.UpdateSeatStatus(model);

            if (result)
            {
                return Ok("Seat status updated successfully.");
            }
            else
            {
                return StatusCode(500, "An error occurred while updating the seat status.");
            }
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] EmailModel emailModel)
        {
            if (emailModel == null || string.IsNullOrEmpty(emailModel.Email))
            {
                return BadRequest("Invalid email data.");
            }

            try
            {
                var body = GenerateEmailBody(emailModel);
                await SendEmailAsync(emailModel.Email, "[TruongCinemas _Thông tin vé phim]- Đặt vé trực tuyến thành công / Your online ticket purchase has been successful", body);
                return Ok("Email đã được gửi thành công.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra: {ex.Message}");
            }
        }
        // Tạo mã QR cho vé
        
        private string GenerateEmailBody(EmailModel emailModel)
        {
            // Định dạng số tiền với dấu chấm
            string formattedTotalPay = emailModel.TotalPay.ToString("N0", new System.Globalization.CultureInfo("vi-VN"));

            return $@"
                <table style=""width:100%;max-width:800px;"">
                    <tbody>
                        <tr>
                            <td align=""center"">
                                <a>
                                    <img style=""height: 304px;width: 270px;"" src='{emailModel.Img}' alt='Image Movie' /></a>
                            </td>
                        </tr>
                        <tr>
                            <td align=""left"">
                                <h1 style=""width: 804px;"">Xin chào {emailModel.Name} / Hello {emailModel.Name}</h1>
                                <table align=""left"">
                                    <tbody>
                                        <tr><td>Phim (Movie):</td><th align=""left"">{emailModel.MovieTitle}</th></tr>                    
                                        <tr><td>Phòng chiếu (Hall):</td><th align=""left"">{emailModel.Theater}</th></tr>
                                        <tr><td>Thời gian (Session):</td><th align=""left"">{emailModel.DayShowtime} {emailModel.TimeShowtime}</th></tr>
                                        <tr><td>Ghế (Seat):</td><th align=""left"">{emailModel.Chair}</th></tr>
                                        <tr><td>Đồ ăn (Food):</td><th align=""left"">{emailModel.Food}</th></tr>
                                        <tr><td>Phương thức thanh toán (Payment method):</td><th align=""left"">Chuyển khoản ngân hàng</th></tr>
                                        <tr><td>Thời gian thanh toán (Time):</td><th align=""left"">{emailModel.timePaypal}</th></tr>
                                        <tr><td>Tổng tiền (Total):</td><th align=""left"">{formattedTotalPay} VND</th></tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
        
                        <tr>
                            <td align=""left"">
                                <p>Lưu ý: Vé đã mua không thể hủy, đổi hoặc trả lại. Vui lòng liên hệ Ban Quản Lý rạp hoặc tra cứu thông tin tại mục F.A.Q để biết thêm chi tiết.</p>
                                <p>Cảm ơn bạn đã lựa chọn Truong Cinemas. Chúc bạn xem phim vui vẻ!</p>
                                    
                            </td>
                        </tr>
                    </tbody>
                </table>";
        }


        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var fromEmail = "nguyenvantruong05032003@gmail.com"; // Địa chỉ email của bạn
            var fromPassword = "wfnc raon gzrn qliz"; // Mật khẩu ứng dụng đã tạo từ Google

            using (var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587))
            {
                client.EnableSsl = true; // Bật SSL
                client.Credentials = new NetworkCredential(fromEmail, fromPassword); // Đăng nhập với thông tin của bạn

                var mailMessage = new MailMessage(fromEmail, toEmail, subject, body)
                {
                    IsBodyHtml = true // Chỉ định rằng nội dung email là HTML
                };

                try
                {
                    await client.SendMailAsync(mailMessage); // Gửi email
                }
                catch (SmtpException smtpEx)
                {
                    throw new Exception($"Email sending failed: {smtpEx.Message}"); // Xử lý ngoại lệ SMTP
                }
                catch (Exception ex)
                {
                    throw new Exception($"An unexpected error occurred: {ex.Message}"); // Xử lý ngoại lệ chung
                }
            }
        }

        public class EmailModel
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string MovieTitle { get; set; }
            public string Theater { get; set; }
            public string DayShowtime { get; set; }
            public string TimeShowtime { get; set; }
            public string Chair { get; set; }
            public string Food { get; set; }
            public decimal TotalPay { get; set; }
            public string Img { get; set; }
            public string timePaypal { get; set; }
            //public string QRCode { get; set; }
        }

    }
}
