using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL
{
    public class FoodModel
    {
        public int IdFood { get; set; }     // idfood, khóa chính
        public string Name { get; set; }     // Tên món ăn
        public decimal Price { get; set; }   // Giá món ăn
    }

}
