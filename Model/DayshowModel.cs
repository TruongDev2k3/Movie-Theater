using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class DayshowModel
    {
        public int DayShowId { get; set; }
        public DateTime DayShowtime { get; set; }
        public int PremiereId { get; set; }
        public string day_of_week { get; set; }
    }
}
