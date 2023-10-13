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
    }
}
