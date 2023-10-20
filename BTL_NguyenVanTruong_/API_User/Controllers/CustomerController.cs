using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.Extensions.Configuration;
using BTL_NguyenVanTruong_.BLL;
using Microsoft.AspNetCore.Mvc;

namespace BTL_NguyenVanTruong_.API_User.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IConfiguration _configuration;
        [Route("Customer")]
        [HttpGet]
        public IActionResult Customers()
        {
            KhachHangBusiness khb = new KhachHangBusiness(_configuration);
            List<KhachHangModel> danhSachKhachHang = new List<KhachHangModel>();
            try
            {
                danhSachKhachHang = khb.GetAllKhachHangs();
            }
            // Khởi tạo đối tượng KhachHangBusiness
            catch(Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            // Gọi phương thức GetAllKhachHangs để lấy danh sách khách hàng
            
            
            

            // Trả về view "Customers" với danh sách khách hàng
            return View(danhSachKhachHang);
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCustomer(KhachHangModel model)
        {
            if (ModelState.IsValid)
            {
                TempData["errorMessage"] = "Dữ liệu không hợp lệ";
            }
            KhachHangBusiness khb = new KhachHangBusiness(_configuration);
            bool result = khb.CreateCustomer(model);
            
            if(!result)
            {
                TempData["errorMessage"] = "Dữ liệu chưa được lưu.";
                return View();
            }
            TempData["successMessage"] = "Thêm thông tin khách hàng thành công";
            return RedirectToAction("Customers");
        }


        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            try
            {
                KhachHangBusiness khb = new KhachHangBusiness(_configuration);
                KhachHangModel khachhang = khb.GetCustomerByID(id);

                if (khachhang.Id == 0)
                {
                    TempData["errorMessage"] = "ID khách hàng không hợp lệ";
                    return RedirectToAction("Customers");
                }
                return View(khachhang);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult EditCustomer(KhachHangModel model)
        {
            try
            {
                KhachHangBusiness khb = new KhachHangBusiness(_configuration);
                //KhachHangModel khachhang = khb.GetCustomerByID(id);

                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Dữ liệu không hợp lệ";
                    return View();
                }
                bool result = khb.UpdateCustomer(model);
                
                if (!result)
                {
                    TempData["errorMessage"] = "Dữ liệu chưa được cập nhập.";
                    return View();
                }
                TempData["successMessage"] = "Dữ liệu đã được cập nhập";
                return RedirectToAction("Customers");
                
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public IActionResult DeleteCustomer(int id)
        {
            try
            {
                KhachHangBusiness khb = new KhachHangBusiness(_configuration);
                KhachHangModel khachhang = khb.GetCustomerByID(id);

                if (khachhang.Id == 0)
                {
                    TempData["errorMessage"] = "ID khách hàng không hợp lệ";
                    return RedirectToAction("Customers");
                }
                return View(khachhang);
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("DeleteCustomer")]
        public IActionResult DeleteCustomerConfirmed(KhachHangModel model)
        {
            try
            {
                KhachHangBusiness khb = new KhachHangBusiness(_configuration);
                //KhachHangModel khachhang = khb.GetCustomerByID(id);

                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Dữ liệu không hợp lệ";
                    return View();
                }
                bool result = khb.DeleteCustomer(model.Id);

                if (!result)
                {
                    TempData["errorMessage"] = "Dữ liệu chưa được xóa.";
                    return View();
                }
                TempData["successMessage"] = "Dữ liệu đã được xóa thành công";
                return RedirectToAction("Customers");

            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

            //API TÌM KIẾM THÔNG TIN KHÁCH HÀNG
            [HttpGet]
            [Route("SearchKhachHang")]
            public IActionResult SearchKhachHang(int pageIndex, int pageSize, string tenkh, string diachi)
            {
                try
                {
                    long total;
                    KhachHangBusiness khb = new KhachHangBusiness(_configuration);

                    // Gọi phương thức GetAllKhachHangs để lấy danh sách khách hàng
                    List<KhachHangModel> danhSachKhachHang = khb.SearchKhachHang(pageIndex, pageSize, out total, tenkh, diachi);

                    if (danhSachKhachHang != null && danhSachKhachHang.Count > 0)
                    {
                        var result = new
                        {
                            Total = total,
                            Data = danhSachKhachHang
                        };
                        return Ok(result);
                    }
                    else
                    {
                        return NotFound("Không tìm thấy khách hàng nào phù hợp hoặc có lỗi xảy ra.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi khi tìm kiếm khách hàng: " + ex.Message);
                    return BadRequest("Lỗi khi tìm kiếm khách hàng.");
                }
            }

        }
    }


