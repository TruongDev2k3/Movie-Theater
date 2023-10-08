using System;
using System.Collections.Generic;
using System.Text;
using BTL_NguyenVanTruong_.Models;
namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IProductRepository
    {
        bool CreateProduct(ProductsModel model);
        bool UpdateProduct(ProductsModel model);
        ProductsModel GetProductByMaSP(int masp);
        bool DeleteProduct(int masp);
        List<ProductsModel> GetListProduct();
    }
}
