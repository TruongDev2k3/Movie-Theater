using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public partial interface ISeatStatusRepository
    {
        List<SeatStatusModel> GetSeatStatus(int movieId, DateTime showDate, TimeSpan showTime);
    }
}
