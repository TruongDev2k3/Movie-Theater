using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public partial interface ICategoryRepository
    {
        bool CreateCategory(CategoryModel model);
        bool UpdateCategory(CategoryModel model);
        CategoryModel GetCategorybyID(int mtl);
        bool DeleteCategory(int mtl);
        List<CategoryModel> GetCategory();
    }
}

