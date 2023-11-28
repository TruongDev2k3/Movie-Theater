using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IAccountBusiness
    {
        bool CreateAccount(AccountModel model);
        bool UpdateAccount(AccountModel model);
        AccountModel GetAccountbyID(int mtk);
        bool DeleteAccount(int mtk);
        List<AccountModel> GetAccount();
        List<AccountModel> SearchAccount(string tk);
    }
}
