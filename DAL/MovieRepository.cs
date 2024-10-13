using BTL_NguyenVanTruong_.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using MODEL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace DAL
{
    public class MovieRepository : IMovieRepository
    {
        SqlConnection _connection = null;
        SqlCommand _command = null;
        public static IConfiguration _configuration { get; set; }

        public MovieRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public List<MovieModel> GetMovie()
        {
            List<MovieModel> movies = new List<MovieModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetMovies"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    MovieModel mv = new MovieModel();
                    {
                        mv.MovieId = (int)reader["movieId"];
                        mv.Title = reader["title"].ToString();
                        mv.Category = reader["category"].ToString();
                        mv.DatePremiere = reader["date_premiere"] != DBNull.Value ? (DateTime?)reader["date_premiere"] : null;
                        mv.Poster = reader["poster"].ToString();
                        mv.Director = reader["director"].ToString();
                        mv.Content = reader["content"].ToString();
                        mv.Trailer = reader["trailer"].ToString();
                        mv.Rating = reader["rating"] != DBNull.Value ? (decimal)reader["rating"] : 0m;
                        mv.Actor = reader["actor"].ToString();
                        movies.Add(mv);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return movies;
        }
        public MovieModel GetMoviebyID(int id)
        {
            // Khởi tạo khachhang
            MovieModel mv = new MovieModel();

            try
            {
                // Lấy chuỗi kết nối csdl
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    _command = connection.CreateCommand();
                    // Định nghĩa kiểu của command là 1 thủ tục lưu trữ (không sử dụng câu lệnh sql)
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "GetMovieById"; // Tên thủ tục lấy thông tin phim

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@movieId", id);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = _command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mv.MovieId = (int)reader["movieId"];
                            mv.Title = reader["title"].ToString();
                            mv.Category = reader["category"].ToString();
                            mv.DatePremiere = reader["date_premiere"] != DBNull.Value ? ((DateTime)reader["date_premiere"]).Date : (DateTime?)null;
                            mv.Poster = reader["poster"].ToString();
                            mv.Director = reader["director"].ToString();
                            mv.Content = reader["content"].ToString();
                            mv.Trailer = reader["trailer"].ToString();
                            mv.Rating = reader["rating"] != DBNull.Value ? (decimal)reader["rating"] : 0m;
                            mv.Actor = reader["actor"].ToString();
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi lấy thông tin bộ phim: " + ex.Message);
            }

            return mv;
        }

        public bool CreateMovie(MovieModel model)
        {
            try
            {
                int rowsAffected = 0;

                // Lấy chuỗi kết nối từ cấu hình
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    _command = connection.CreateCommand();
                    // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                    _command.CommandType = CommandType.StoredProcedure;
                    // Tên của thủ tục lưu trữ
                    _command.CommandText = "AddMovie";

                    // Định nghĩa các tham số cho thủ tục lưu trữ, gán các giá trị từ model
                    _command.Parameters.AddWithValue("@title", model.Title);
                    _command.Parameters.AddWithValue("@category", model.Category);
                    _command.Parameters.AddWithValue("@date_premiere", model.DatePremiere.HasValue ? (object)model.DatePremiere.Value : DBNull.Value);
                    _command.Parameters.AddWithValue("@poster", model.Poster);
                    _command.Parameters.AddWithValue("@director", model.Director);
                    _command.Parameters.AddWithValue("@content", model.Content);
                    _command.Parameters.AddWithValue("@trailer", model.Trailer);
                    _command.Parameters.AddWithValue("@rating", model.Rating);
                    _command.Parameters.AddWithValue("@actor", model.Actor);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    rowsAffected = _command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được thêm vào không
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi thêm phim: " + ex.Message);
                return false;
            }
        }



        // Sửa thông tin phim
        public bool UpdateMovie(MovieModel model)
        {
            try
            {
                int rowsAffected = 0;

                // Lấy chuỗi kết nối từ cấu hình
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    _command = connection.CreateCommand();
                    // Kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                    _command.CommandType = CommandType.StoredProcedure;
                    // Tên của thủ tục lưu trữ
                    _command.CommandText = "UpdateMovie";

                    // Định nghĩa các tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@movieId", model.MovieId); // MovieId để xác định phim nào cần update
                    _command.Parameters.AddWithValue("@title", model.Title);
                    _command.Parameters.AddWithValue("@category", model.Category);
                    _command.Parameters.AddWithValue("@date_premiere", model.DatePremiere.HasValue ? (object)model.DatePremiere.Value : DBNull.Value);
                    _command.Parameters.AddWithValue("@poster", model.Poster);
                    _command.Parameters.AddWithValue("@director", model.Director);
                    _command.Parameters.AddWithValue("@content", model.Content);
                    _command.Parameters.AddWithValue("@trailer", model.Trailer);
                    _command.Parameters.AddWithValue("@rating", model.Rating);
                    _command.Parameters.AddWithValue("@actor", model.Actor);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    rowsAffected = _command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được cập nhật không
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi cập nhật phim: " + ex.Message);
                return false;
            }
        }

        // Lấy thông tin phim theo id phim


        // Xóa thông tin phim theo id phim
        public bool DeleteMovie(int movieId)
        {
            try
            {
                int rowsAffected = 0;

                // Lấy chuỗi kết nối từ cấu hình
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    _command = connection.CreateCommand();
                    // Kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                    _command.CommandType = CommandType.StoredProcedure;
                    // Tên của thủ tục lưu trữ
                    _command.CommandText = "DeleteMovie";

                    // Định nghĩa tham số cho thủ tục lưu trữ (ID của phim cần xóa)
                    _command.Parameters.AddWithValue("@movieId", movieId);

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    rowsAffected = _command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được xóa không
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi xóa phim: " + ex.Message);
                return false;
            }
        }

        public MovieModel GetTrailerbyID(int id)
        {
            // Khởi tạo khachhang
            MovieModel mv = new MovieModel();

            try
            {
                // Lấy chuỗi kết nối csdl
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    _command = connection.CreateCommand();
                    // Định nghĩa kiểu của command là 1 thủ tục lưu trữ (không sử dụng câu lệnh sql)
                    _command.CommandType = CommandType.StoredProcedure;
                    _command.CommandText = "GetTrailerById"; // Tên thủ tục lấy thông tin phim

                    // Định nghĩa tham số cho thủ tục lưu trữ
                    _command.Parameters.AddWithValue("@movieId", id);

                    // Sử dụng SqlDataReader để đọc dữ liệu từ thủ tục lưu trữ
                    using (var reader = _command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            mv.Trailer = reader["trailer"].ToString();
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi lấy thông tin bộ phim: " + ex.Message);
            }

            return mv;
        }
        public List<FilmAndShowTimeModel> GetShowtimesByDate(string date)
        {
            var movieList = new List<FilmAndShowTimeModel>();

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetShowtimesByDate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@date", date);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var movieTitle = reader["MovieTitle"].ToString();
                                var showtimes = reader["Showtimes"].ToString()
                                                  .Split(new[] { ", " }, StringSplitOptions.None)
                                                  .ToList();

                                var movie = movieList.FirstOrDefault(m => m.Title == movieTitle);
                                if (movie == null)
                                {
                                    movie = new FilmAndShowTimeModel
                                    {
                                        MovieId = (int)reader["movieId"],
                                        Title = movieTitle,
                                        Showtimes = new List<string>(),
                                        DatePremiere = reader["date_premiere"] != DBNull.Value ? (DateTime?)reader["date_premiere"] : null,
                                        Poster = reader["poster"].ToString(),
                                        Content = reader["content"].ToString(),
                                        DayShowtime = reader["day_showtime"] != DBNull.Value ? (DateTime)reader["day_showtime"] : DateTime.MinValue
                                    };
                                    movieList.Add(movie);
                                }

                                movie.Showtimes.AddRange(showtimes);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin khung giờ: " + ex.Message);
            }

            return movieList;
        }
        public List<DayshowModel> GetMovieShowDays(int movieId)
        {
            List<DayshowModel> dayShowList = new List<DayshowModel>();

            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("GetMovieShowDays", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MovieId", movieId);

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var dayShow = new MODEL.DayshowModel
                            {
                                DayShowtime = reader.GetDateTime(reader.GetOrdinal("day_showtime")),
                                day_of_week = reader.GetString(reader.GetOrdinal("day_of_week"))
                            };
                            dayShowList.Add(dayShow);
                        }
                    }
                }
            }

            return dayShowList;
        }
        public List<PremiereModel> GetShowtimesByMovieAndDate(int movieId, DateTime dayShowtime)
        {
            var showtimes = new List<PremiereModel>();

            try
            {
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetShowtimesByMovieAndDate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MovieId", movieId);
                        command.Parameters.AddWithValue("@DayShowtime", dayShowtime);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var premiere = new PremiereModel
                                {
                                    
                                    Time = (TimeSpan)reader["ShowTime"]
                                };
                                showtimes.Add(premiere);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi ở đây (ví dụ: ghi log lỗi)
                Console.WriteLine("Lỗi khi lấy khung giờ chiếu phim: " + ex.Message);
            }

            return showtimes;
        }
        public bool OrderTicket(TicketModel model)
        {

            try
            {
                int rowsAffected = 0;
                // Lấy chuỗi kết nối từ cấu hình
                using (var connection = new SqlConnection(GetConnectionString()))
                {
                    connection.Open();

                    // Tạo một đối tượng SqlCommand để gọi thủ tục lưu trữ
                    _command = connection.CreateCommand();
                    // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                    _command.CommandType = CommandType.StoredProcedure;
                    // Tên của thủ tục lưu trữ
                    _command.CommandText = "InsertDataTicket";
                    _command.Parameters.AddWithValue("@MaTaiKhoan", model.MaTaiKhoan);
                    _command.Parameters.AddWithValue("@movieId", model.MovieId);
                    _command.Parameters.AddWithValue("@idfood", model.IdFood ?? (object)DBNull.Value);
                    _command.Parameters.AddWithValue("@chairName", model.ChairName);
                    _command.Parameters.AddWithValue("@showtime", model.ShowTime);
                    _command.Parameters.AddWithValue("@showdate", model.ShowDate);
                    _command.Parameters.AddWithValue("@quantity_ticket", model.QuantityTicket);
                    _command.Parameters.AddWithValue("@total_price", model.TotalPrice);
                    // Định nghĩa các tham số cho thủ tục lưu trữ

                    // Thực hiện thủ tục lưu trữ và lấy số hàng bị ảnh hưởng
                    rowsAffected = _command.ExecuteNonQuery();

                    // Kiểm tra xem có bản ghi nào đã được thêm vào không
                    return rowsAffected > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                // Xử lý các ngoại lệ (ví dụ: log lại lỗi)
                Console.WriteLine("Lỗi khi thêm phim: " + ex.Message);
                return false;
            }
        }
        public List<MovieModel> GetTopMovie()
        {
            List<MovieModel> movies = new List<MovieModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetHotMoviesByRating"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    MovieModel mv = new MovieModel();
                    {
                        mv.MovieId = (int)reader["movieId"];
                        mv.Title = reader["title"].ToString();
                        mv.Category = reader["category"].ToString();
                        mv.DatePremiere = reader["date_premiere"] != DBNull.Value ? (DateTime?)reader["date_premiere"] : null;
                        mv.Poster = reader["poster"].ToString();
                        mv.Director = reader["director"].ToString();
                        mv.Content = reader["content"].ToString();
                        mv.Trailer = reader["trailer"].ToString();
                        mv.Rating = reader["rating"] != DBNull.Value ? (decimal)reader["rating"] : 0m;
                        mv.Actor = reader["actor"].ToString();
                        movies.Add(mv);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return movies;
        }
        public List<MovieModel> GetMoviesNowShowing()
        {
            List<MovieModel> movies = new List<MovieModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetMoviesNowShowing"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    MovieModel mv = new MovieModel();
                    {
                        mv.MovieId = (int)reader["movieId"];
                        mv.Title = reader["title"].ToString();
                        mv.Category = reader["category"].ToString();
                        mv.DatePremiere = reader["date_premiere"] != DBNull.Value ? (DateTime?)reader["date_premiere"] : null;
                        mv.Poster = reader["poster"].ToString();
                        mv.Director = reader["director"].ToString();
                        mv.Content = reader["content"].ToString();
                        mv.Trailer = reader["trailer"].ToString();
                        mv.Rating = reader["rating"] != DBNull.Value ? (decimal)reader["rating"] : 0m;
                        mv.Actor = reader["actor"].ToString();
                        movies.Add(mv);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return movies;
        }
        public List<MovieModel> GetUpcomingMovies()
        {
            List<MovieModel> movies = new List<MovieModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetUpcomingMovies"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    MovieModel mv = new MovieModel();
                    {
                        mv.MovieId = (int)reader["movieId"];
                        mv.Title = reader["title"].ToString();
                        mv.Category = reader["category"].ToString();
                        mv.DatePremiere = reader["date_premiere"] != DBNull.Value ? (DateTime?)reader["date_premiere"] : null;
                        mv.Poster = reader["poster"].ToString();
                        mv.Director = reader["director"].ToString();
                        mv.Content = reader["content"].ToString();
                        mv.Trailer = reader["trailer"].ToString();
                        mv.Rating = reader["rating"] != DBNull.Value ? (decimal)reader["rating"] : 0m;
                        mv.Actor = reader["actor"].ToString();
                        movies.Add(mv);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return movies;
        }
        public List<DoanhThuModel> GetProceeds()
        {
            List<DoanhThuModel> dt = new List<DoanhThuModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "sp_ThongKeDoanhSoTungPhim"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    DoanhThuModel mv = new DoanhThuModel();
                    {                      
                        mv.MovieId = (int)reader["movieId"];
                        mv.TitleMovie = reader["titlemovie"].ToString();
                        mv.poster = reader["poster"].ToString();
                        mv.TongSoLuongVe = (int)reader["TongSoLuongVe"];
                        mv.TongDoanhThu = (decimal)reader["TongDoanhThu"];

                        dt.Add(mv);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return dt;
        }
        public List<Data_TicketModel> getDataTicket()
        {
            List<Data_TicketModel> dt = new List<Data_TicketModel>();

            using (var connection = new SqlConnection(GetConnectionString()))
            {
                // Mở kết nối
                connection.Open();
                // Tạo một đối tượng SqlCommand để gọi stored procedure
                _command = connection.CreateCommand();
                // kiểu cmd là 1 hàm thủ tục không phải câu lệnh sql
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "GetAllData_Ticket"; // Tên stored procedure
                // Thực hiện truy vấn và lấy kết quả (ExecuteReader trả về  SqlDataReader dùng đọc dữ liệu từ sql)
                SqlDataReader reader = _command.ExecuteReader();
                // Đọc dữ liệu từ kết quả trả về
                while (reader.Read())
                {
                    Data_TicketModel mv = new Data_TicketModel();
                    {
        
                        mv.titlemovie = reader["titlemovie"].ToString();
                        mv.foodname = reader["foodname"].ToString();
                        mv.chairName = reader["chairName"].ToString();
                        mv.theaterName = reader["theaterName"].ToString();
                        mv.showtime = reader["showtime"].ToString();
                        mv.showdate = reader["showdate"] != DBNull.Value ? (DateTime?)reader["showdate"] : null;
                        mv.quantity_ticket = (int)reader["quantity_ticket"];
                        mv.total_price = (decimal)reader["total_price"];

                        dt.Add(mv);
                    }
                }
                connection.Close();
                reader.Close();

            }
            return dt;
        }

    }
}
