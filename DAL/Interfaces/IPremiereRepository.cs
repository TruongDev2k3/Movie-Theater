using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public partial interface IPremiereRepository
    {
        //bool CreateAccount(AccountModel model);
        //bool UpdateAccount(AccountModel model);
        //AccountModel GetAccountbyID(int mtk);
        //bool DeleteAccount(int mtk);
        List<PremiereModel> GetPremiere();
        //List<AccountModel> SearchAccount(string tk);
    }
}
