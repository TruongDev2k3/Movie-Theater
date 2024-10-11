using DAL.Interfaces;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DAL
{
    public class PremiereRepository : IPremiereRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public PremiereRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }
        public List<PremiereModel> GetPremiere()
        {
            List<PremiereModel> pm = new List<PremiereModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetAllPremiereTimes"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    PremiereModel ac = new PremiereModel();
                    {
                        ac.PremiereId = Convert.ToInt32(reader["premiereId"]);
                        ac.Time = (TimeSpan)reader["time"];
                        pm.Add(ac);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return pm;
        }

    }
}
