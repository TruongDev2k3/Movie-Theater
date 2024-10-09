using BLL.Interfaces;
using DAL;
using Microsoft.Extensions.Configuration;
using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryBusiness : ICategoryBusiness
    {
        private readonly IConfiguration _configuration;

        private CategoryRepository _res; // Không cần khởi tạo ở đây

        public CategoryBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new CategoryRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

        public bool CreateCategory(CategoryModel model)
        {
            return _res.CreateCategory(model);
        }
        public bool UpdateCategory(CategoryModel model)
        {
            return _res.UpdateCategory(model);
        }
        public bool DeleteCategory(int mcm)
        {
            return _res.DeleteCategory(mcm);
        }
        public CategoryModel GetCategorybyID(int mtk)
        {
            return _res.GetCategorybyID(mtk);
        }
        public List<CategoryModel> GetCategory()
        {
            return _res.GetCategory();
        }
        
    }
}
