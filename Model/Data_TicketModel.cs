using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class Data_TicketModel
    {
        public string titlemovie {  get; set; }
        public string foodname { get; set; }
        public string chairName { get; set; }
        public string theaterName { get; set; }
        public string showtime { get; set; }
        public DateTime? showdate { get; set; }
        public int quantity_ticket { get; set; }
        public decimal total_price { get; set; }
        public string qrcode { get; set; }
    }
}
