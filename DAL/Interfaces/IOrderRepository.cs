using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
using DAL.Helper;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using System.Data.SqlClient;
using System.IO;
namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IOrderRepository
    {
        int AddCustomerAndOrder(OrderDataModel odm);
        int AddCustomer(SqlConnection connection, SqlTransaction transaction, OrderDataModel odm);
        int AddOrder(SqlConnection connection, SqlTransaction transaction, int customerId, OrderDataModel odm);
        void AddOrderDetails(SqlConnection connection, SqlTransaction transaction, int orderId, string listProduct);
    }
}
