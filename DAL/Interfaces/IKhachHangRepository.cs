using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IKhachHangRepository
    {
        bool CreateCustomer(CustomerModel model);
        bool UpdateCustomer(CustomerModel model);
        CustomerModel GetCustomerByID(int id);
        bool DeleteCustomer(int id);
        List<CustomerModel> GetAllKhachHangs();
        List<CustomerModel> SearchKhachHang(string tukhoa);
    }
}
