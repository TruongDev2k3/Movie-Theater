using BTL_NguyenVanTruong_.Models;
namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IProductBusiness
    {
        bool CreateProduct(ProductsModel model);
        bool UpdateProduct(ProductsModel model);
        ProductsModel GetProductByMaSP(int masp);
        bool DeleteProduct(int masp);
        List<ProductsModel> GetListProduct();
        List<ProductsModel> GetListMacProduct();
        List<ProductsModel> GetListIphoneProduct();
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
