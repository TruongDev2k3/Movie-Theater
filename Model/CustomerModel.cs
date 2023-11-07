using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MODEL
{
    public class CustomerModel
    {
		[Key]
		[Required]
		public int Id { get; set; }
		[Required]
		public string TenKH { get; set; }
		[Required]
		public string GioiTinh { get; set; }
		[Required]
		public string DiaChi { get; set; }
		[Required]
		public string SDT { get; set; }
		[Required]
		public string Email { get; set; }
	}
}
