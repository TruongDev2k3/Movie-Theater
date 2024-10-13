using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class UpdateSeatAdmin
    {
        public int MovieId { get; set; }
        public DateTime ShowDate { get; set; }
        public string ShowTime { get; set; }
        public int TheaterId { get; set; }
        public string SeatNames { get; set; }
    }
}
