using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public partial interface IDayshowtimeBusiness
    {
        //bool CreateDayshowtime(DayshowModel model);
        //bool UpdateDayshowtime(DayshowModel model);
        //bool DeleteDayshowtime(int mtk);
        List<DayshowModel> GetDayshowtime();
    }
}
