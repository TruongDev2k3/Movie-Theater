using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class TheaterModel
    {
        public int TheaterId { get; set; } // Primary key, auto-incremented
        public string TheaterName { get; set; } // Theater name, not null
        public string Description { get; set; } // Description of the theater
        public string ImageTheater { get; set; } // Image URL or path for the theater
    }

}
