using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class MovieModel
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime? DatePremiere { get; set; }
        public string Poster { get; set; }
        public string Director { get; set; }
        public string Content { get; set; }
        public string Trailer { get; set; }
        public decimal Rating { get; set; }
        public string Actor { get; set; }
    }

}
