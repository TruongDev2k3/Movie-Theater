using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IProductRepository
    {
        bool CreateProduct(ProductsModel model);
        bool UpdateProduct(ProductsModel model);
        ProductsModel GetProductByMaSP(int masp);
        bool DeleteProduct(int masp);
        List<ProductsModel> GetListProduct(); // done
        List<ProductsModel> SearchProduct(string tensp);
    }
}
