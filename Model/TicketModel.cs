using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    
    public class TicketModel
    {
        public int IdTicket { get; set; }
        public int MaTaiKhoan { get; set; }
        public int MovieId { get; set; }
        public string TitleMovie { get; set; }
        public int? IdFood { get; set; }
        public string FoodName { get; set; }
        public string ChairName { get; set; }
        public string TheaterName { get; set; }
        public string ShowTime { get; set; }
        public DateTime ShowDate { get; set; }
        public int QuantityTicket { get; set; }
        public decimal TotalPrice { get; set; }
        
    }

}
