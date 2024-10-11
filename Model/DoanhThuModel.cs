using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class DoanhThuModel
    {
        public int MovieId { get; set; }         // ID của phim
        public string TitleMovie { get; set; }
        public string poster { get; set; } // Tên phim
        public int TongSoLuongVe { get; set; }    // Tổng số lượng vé bán ra
        public decimal TongDoanhThu { get; set; }
    }
}
