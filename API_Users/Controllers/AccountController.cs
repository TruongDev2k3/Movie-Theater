using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;

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

        [HttpPost("create-acc")]
        public ActionResult CreateAccount([FromBody] AccountModel model)
        {
            var result = _acc.CreateAccount(model);
            return Ok(result);
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
        [HttpPost("searchacc")]
        public ActionResult<List<AccountModel>> SearchAccount(string tukhoa)
        {
            try
            {
                var accList = _acc.SearchAccount(tukhoa);

                if (accList == null || accList.Count == 0)
                {
                    return NotFound("Danh sách tài khoản trống");
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
