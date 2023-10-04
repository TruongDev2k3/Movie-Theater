using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using BTL_NguyenVanTruong_.DAL;
namespace BTL_NguyenVanTruong_.BLL
{
    public partial class TypeAccountBusiness : ITypeAccpuntBusiness
    {
        private readonly IConfiguration _configuration;

        private TypeAccountRepository _res; // Không cần khởi tạo ở đây

        public TypeAccountBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new TypeAccountRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }
        public List<TypeAccountModel> GetTypeAccount()
        {
            return _res.GetTypeAccount();
        }
    }
}
