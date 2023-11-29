using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IQuangCaoBusiness
    {
        bool CreateQuangCao(QuangCaoModel model);
        bool UpdateQuangCao(QuangCaoModel model);
        QuangCaoModel GetQCbyID(int id);
        bool DeleteQuangCao(int id);
        List<QuangCaoModel> GetQuangCao();
    }
}
