using MODEL;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DAL;
namespace BTL_NguyenVanTruong_.BLL
{
    public class AccountBusiness : IAccountBusiness
    {

        private readonly IConfiguration _configuration;

        private AccountRepository _res; // Không cần khởi tạo ở đây

        public AccountBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new AccountRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }

        public bool CreateAccount(AccountModel model, out string errorMessage)
        {
            return _res.CreateAccount(model,out errorMessage);
        }
        public bool UpdateAccount(AccountModel model)
        {
            return _res.UpdateAccount(model);
        }
        public bool DeleteAccount(int mcm)
        {
            return _res.DeleteAccount(mcm);
        }
        public AccountModel GetAccountbyID(int mtk)
        {
            return _res.GetAccountbyID(mtk);
        }
        public List<AccountModel> GetAccount()
        {
            return _res.GetAccount();
        }
        
    }
}
