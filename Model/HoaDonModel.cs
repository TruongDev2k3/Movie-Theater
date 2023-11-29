using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class HoaDonModel
	{
        public int MaHoaDon { get; set; }
        public DateTime NgayTao { get; set; }
        public DateTime NgayDuyet { get; set; }
        public decimal TongGia { get; set; }
		public int Id { get; set; }
		public string TenKH { get; set; }
		public string GioiTinh { get; set; }
		public string DiaChi { get; set; }
		public string SDT { get; set; }
		public string Email { get; set; }
	}
}
