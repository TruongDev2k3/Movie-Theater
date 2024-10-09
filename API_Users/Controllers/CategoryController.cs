using BLL.Interfaces;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MODEL;

namespace API_Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private ICategoryBusiness _acc;
        public CategoryController(ICategoryBusiness acc)
        {
            _acc = acc;
        }

        [HttpGet("getlistcategory")]
        public ActionResult<List<CategoryModel>> GetCategory()
        {
            try
            {
                var accList = _acc.GetCategory();

                if (accList == null || accList.Count == 0)
                {
                    return NotFound("Danh sách thể loại trống");
                }

                return Ok(accList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }



        [HttpGet("getbyid/{mtl}")]
        public ActionResult<CategoryModel> GetCategorybyID(int mtl)
        {
            var acc = _acc.GetCategorybyID(mtl);

            if (acc == null)
            {
                return NotFound();
            }

            return Ok(acc);
        }


        [HttpPost("create-category")]
        public ActionResult CreateCategory([FromBody] CategoryModel model)
        {
            var result = _acc.CreateCategory(model);
            return Ok(result);
        }

        [HttpPut("update-acc")]
        public ActionResult UpdateCategory([FromBody] CategoryModel model)
        {
            var result = _acc.UpdateCategory(model);
            return Ok(result);
        }

        [HttpDelete("delete-acc/{mtk}")]
        public ActionResult DeleteCategory(int mtk)
        {
            var result = _acc.DeleteCategory(mtk);
            return Ok(result);
        }
        
        
    }
}
