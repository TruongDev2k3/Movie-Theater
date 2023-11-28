using MODEL;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
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

        public bool CreateAccount(AccountModel model)
        {
            return _res.CreateAccount(model);
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
        public List<AccountModel> SearchAccount(string tk)
        {
            return _res.SearchAccount(tk);
        }
    }
}
