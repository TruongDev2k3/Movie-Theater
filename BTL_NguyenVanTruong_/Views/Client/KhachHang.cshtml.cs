using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.BLL;
namespace BTL_NguyenVanTruong_.Views.Client
{
    public class IndexModel : PageModel
    {
        public readonly IConfiguration _configuration;

        public List<KhachHangModel> danhSachKhachHang = new List<KhachHangModel>();
        public void OnGet()
        {
            try
            {
                KhachHangBusiness khb = new KhachHangBusiness(_configuration);

                // Gọi phương thức GetAllKhachHangs để lấy danh sách khách hàng
                List<KhachHangModel> danhSachKhachHang = khb.GetAllKhachHangs();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception : " + ex.ToString());
            }
        }
    }
}
