using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public partial interface IPremiereBusiness
    {
        List<PremiereModel> GetPremiere();
    }
}
