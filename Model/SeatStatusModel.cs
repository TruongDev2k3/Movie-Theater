using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class SeatStatusModel
    {
        public string Title { get; set; }         // Tên phim
        public DateTime ShowDate { get; set; }    // Ngày chiếu
        public TimeSpan ShowTime { get; set; }    // Khung giờ chiếu
        public int TheaterId { get; set; }        // ID rạp
        public string TheaterName { get; set; }   // Tên rạp
        public int SeatId { get; set; }           // ID ghế
        public string SeatName { get; set; }      // Tên ghế
        public bool SeatStatus { get; set; }      // Trạng thái ghế (0: trống, 1: đã đặt)
    }

}
