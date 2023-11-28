using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IAccountRepository
    {
        bool CreateAccount(AccountModel model);
        bool UpdateAccount(AccountModel model);
        AccountModel GetAccountbyID(int mtk);
        bool DeleteAccount(int mtk);
        List<AccountModel> GetAccount();
        List<AccountModel> SearchAccount(string tk);
    }
}
