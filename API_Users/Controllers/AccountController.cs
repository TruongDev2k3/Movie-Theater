using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using System.Net.Mail;
using System.Net;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IAccountBusiness _acc;
        public AccountController(IAccountBusiness acc)
        {
            _acc = acc;
        }

        [HttpGet("getlistacc")]
        public ActionResult<List<AccountModel>> GetAccount()
        {
            try
            {
                var accList = _acc.GetAccount();

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



        [HttpGet("getbyid/{mtk}")]
        public ActionResult<AccountModel> GetAccountbyID(int mtk)
        {
            var acc = _acc.GetAccountbyID(mtk);

            if (acc == null)
            {
                return NotFound();
            }

            return Ok(acc);
        }


        [HttpPost("createaccount")]
        public IActionResult CreateAccount([FromBody] AccountModel model)
        {
            if (model == null)
                return BadRequest("Dữ liệu không hợp lệ.");

            string errorMessage;
            bool result = _acc.CreateAccount(model, out errorMessage);

            if (!result)
            {
                // Trả về lỗi cho client
                return BadRequest(new { message = errorMessage });
            }

            return Ok(new { message = "Tạo tài khoản thành công." });
        }


        [HttpPut("update-acc")]
        public ActionResult UpdateAccount([FromBody] AccountModel model)
        {
            var result = _acc.UpdateAccount(model);
            return Ok(result);
        }

        [HttpDelete("delete-acc/{mtk}")]
        public ActionResult DeleteProduct(int mtk)
        {
            var result = _acc.DeleteAccount(mtk);
            return Ok(result);
        }
        // DTOs
        public class RegisterRequestDto
        {
           
            public string Email { get; set; }
            public string VerificationCode { get; set; }
        }

        public class EmailDto
        {
            public string Email { get; set; }
        }

        // Temporary in-memory store (replace with Redis or DB in real app)
        public static class VerificationStore
        {
            public static Dictionary<string, (string Code, DateTime CreatedAt)> EmailCodes = new();
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
        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] EmailDto payload)
        {
            string email = payload.Email;
            string code = new Random().Next(100000, 999999).ToString("D6");

            VerificationStore.EmailCodes[email] = (code, DateTime.UtcNow);
            await SendEmailAsync(email, "Mã xác nhận đăng ký", $"Mã xác nhận của bạn là: {code}");

            return Ok(new { success = true, message = "Mã xác nhận đã được gửi." });
        }
        //[HttpPost("send-code")]
        //public async Task<IActionResult> SendCode([FromBody] dynamic payload)
        //{
        //    string email = payload.email;
        //    string code = new Random().Next(000000, 999999).ToString("D6");

        //    // Lưu mã vào store
        //    VerificationStore.EmailCodes[email] = code;

        //    // Gửi email thực tế
        //    await SendEmailAsync(email, "Mã xác nhận đăng ký", $"Mã xác nhận của bạn là: {code}");

        //    return Ok(new { success = true });
        //}
      

        [HttpPost("confirm")]
        public IActionResult Confirm([FromBody] RegisterRequestDto dto)
        {
            if (!VerificationStore.EmailCodes.TryGetValue(dto.Email, out var storedEntry))
            {
                return BadRequest(new { success = false, message = "Không tìm thấy mã xác nhận cho email này." });
            }

            // Kiểm tra thời gian hết hạn (ví dụ: 5 phút)
            var timeElapsed = DateTime.UtcNow - storedEntry.CreatedAt;
            if (timeElapsed.TotalMinutes > 5)
            {
                VerificationStore.EmailCodes.Remove(dto.Email);
                return BadRequest(new { success = false, message = "Mã xác nhận đã hết hạn. Vui lòng yêu cầu lại mã mới." });
            }

            if (storedEntry.Code != dto.VerificationCode)
            {
                return BadRequest(new { success = false, message = "Mã xác nhận không đúng." });
            }

            // TODO: Lưu thông tin người dùng vào CSDL tại đây nếu cần.

            VerificationStore.EmailCodes.Remove(dto.Email); // Xóa mã sau khi xác thực thành công

            return Ok(new { success = true, message = "Xác nhận thành công." });
        }

        // API Controller
        //[ApiController]
        //[Route("api/register")]
        //public class RegisterController : ControllerBase
        //{
        //    [HttpPost("send-code")]
        //    public IActionResult SendCode([FromBody] dynamic payload)
        //    {
        //        string email = payload.email;
        //        string code = new Random().Next(100000, 999999).ToString();

        //        // Save to store
        //        VerificationStore.EmailCodes[email] = code;

        //        // TODO: Send email via SMTP or email service
        //        Console.WriteLine($"[DEBUG] Send code {code} to {email}");

        //        return Ok(new { success = true, message = "Mã xác nhận đã được gửi." });
        //    }

        //    [HttpPost("confirm")]
        //    public IActionResult Confirm([FromBody] RegisterRequestDto dto)
        //    {
        //        if (!VerificationStore.EmailCodes.TryGetValue(dto.Email, out var storedCode) || storedCode != dto.VerificationCode)
        //        {
        //            return BadRequest(new { success = false, message = "Mã xác nhận không đúng hoặc đã hết hạn." });
        //        }

        //        // TODO: Save user to DB, check if email exists, etc.
        //        VerificationStore.EmailCodes.Remove(dto.Email); // Remove code after successful use
        //        return Ok(new { success = true });
        //    }
        //}


    }
}
