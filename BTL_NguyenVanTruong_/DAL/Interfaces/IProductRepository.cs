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
        List<ProductsModel> GetListProduct(); // done
        List<ProductsModel> GetListMacProduct(); // done 1
        List<ProductsModel> GetListIphoneProduct(); // done 6
        List<ProductsModel> GetListOppoProduct(); //9
        List<ProductsModel> GetListASUSProduct(); // 2
        List<ProductsModel> GetLGTVProduct(); // 8
        List<ProductsModel> GetPanasonicProduct(); //13
        List<ProductsModel> GetAppleWatchProduct(); //14
        List<ProductsModel> GetDELLProduct(); //4
        List<ProductsModel> GetMSIProduct(); //5
        List<ProductsModel> GetSamsungProduct(); //16


    }
}
