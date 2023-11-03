using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class ProductsModel
    {
        public int MaSanPham { get; set; }
        public int MaChuyenMuc { get; set; }
        public string TenSanPham { get; set; }
        public string AnhSanPham { get; set; }
        public decimal Gia { get; set; }
        public decimal GiaGiam { get; set; }
        public int SoLuong { get; set; }
        public int SoLuongDaBan { get; set; }
    }
}

