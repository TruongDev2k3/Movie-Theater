using MODEL;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Sockets;
using System.Security.Claims;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Collections.Generic;
using DAL;
using System;


namespace BTL_NguyenVanTruong_.BLL
{
    public partial class OrderBusiness : IOrderBusiness
    {
        private readonly IConfiguration _configuration;

        private OrderRepository _res; // Không cần khởi tạo ở đây

        public OrderBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new OrderRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }
        public int AddCustomerAndOrder(OrderDataModel odm)
        {
            return _res.AddCustomerAndOrder(odm);
        }
        public int AddCustomer(SqlConnection connection, SqlTransaction transaction, OrderDataModel odm)
        {
            return _res.AddCustomer(connection, transaction, odm);
        }
        public int AddOrder(SqlConnection connection, SqlTransaction transaction, int customerId, OrderDataModel odm)
        {
            return _res.AddOrder(connection, transaction, customerId, odm);
        }
        public void AddOrderDetails(SqlConnection connection, SqlTransaction transaction, int orderId, string listProduct)
        {
            _res.AddOrderDetails(connection, transaction, orderId, listProduct);
        }
    }
}
