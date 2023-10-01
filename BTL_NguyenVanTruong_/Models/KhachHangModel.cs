using System.ComponentModel.DataAnnotations;
namespace BTL_NguyenVanTruong_.Models
{
	public class KhachHangModel
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
