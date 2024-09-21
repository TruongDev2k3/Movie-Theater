using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class FilmAndShowTimeModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public List<string> Showtimes { get; set; }
        public DateTime? DatePremiere { get; set; }
        public string Poster { get; set; }
        public string Content { get; set; }
        public DateTime DayShowtime { get; set; }
    }
}
