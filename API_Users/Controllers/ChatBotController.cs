using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

[Route("api/chatbot")]
[ApiController]
public class ChatbotController : ControllerBase
{
    public static IConfiguration _configuration { get; set; }
    public ChatbotController(IConfiguration configuration) {
        _configuration = configuration;
    }
    private string GetConnectionString()
    {
        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
        _configuration = builder.Build();
        return _configuration.GetConnectionString("DefaultConnection");
    }

    [HttpPost]
    public async Task<IActionResult> GetChatbotResponse([FromBody] ChatRequest request)
    {
        if (string.IsNullOrEmpty(request.Query))
        {
            return BadRequest(new { reply = "Xin lỗi, tôi không hiểu yêu cầu của bạn." });
        }

        string response = await ProcessUserQuery(request.Query);
        return Ok(new { reply = response });
    }

    private async Task<string> ProcessUserQuery(string query)
    {
        query = query.ToLower();
        if (query.Contains("đặt vé") || query.Contains("dat ve"))
        {
            return "Vui lòng chọn : Chọn phim --> Chọn ngày --> Chọn suất\nBạn còn thắc mắc gì không?";
        }
        if (query.Contains("lịch chiếu hôm nay") || query.Contains("lich chieu hom nay"))
        {
            return await GetSchedule(DateTime.Now);
        }
        if (query.Contains("lịch chiếu ngày"))
        {
            DateTime date;
            if (DateTime.TryParse(query.Replace("lịch chiếu ngày", "").Trim(), out date))
            {
                return await GetSchedule(date);
            }
        }
        if (query.Contains("giá vé") || query.Contains("gia ve"))
        {
            return "Người lớn: 65000đ, Trẻ em: 25000đ \nTôi có thể giúp gì bạn nữa không?";
        }
        //if (query.Contains("phim đang chiếu"))
        //{
        //    return await GetMovies("now_playing");
        //}
        //if (query.Contains("phim sắp chiếu"))
        //{
        //    return await GetMovies("coming_soon");
        //}
        //if (query.Contains("top phim hay"))
        //{
        //    return await GetTopMovies();
        //}
        //if (query.Contains("khung giờ chiếu"))
        //{
        //    return await GetShowtimes();
        //}
        if (query.Contains("trailer"))
        {
            return await GetMovieTrailer(query);
        }
        if (query.Contains("chi tiết phim"))
        {
            return await GetMovieDetails(query);
        }

        return "Xin lỗi, tôi không hiểu yêu cầu của bạn.";
    }

    private async Task<string> GetSchedule(DateTime date)
    {
        using (SqlConnection conn = new SqlConnection(GetConnectionString()))
        {
            await conn.OpenAsync();
            using (SqlCommand cmd = new SqlCommand("GetShowtimesByDate", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Date", date);

                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    Dictionary<string, List<string>> schedule = new Dictionary<string, List<string>>();

                    while (await reader.ReadAsync())
                    {
                        string movieTitle = reader["MovieTitle"]?.ToString() ?? "N/A";
                        string rawTime = reader["Showtimes"]?.ToString() ?? "N/A";

                        // Định dạng lại thời gian từ "HH:mm:ss" -> "HH:mm"
                        if (TimeSpan.TryParse(rawTime, out TimeSpan timeSpan))
                        {
                            rawTime = timeSpan.ToString(@"hh\:mm");
                        }
                        else if (DateTime.TryParse(rawTime, out DateTime dateTime))
                        {
                            rawTime = dateTime.ToString("HH:mm");
                        }

                        if (!schedule.ContainsKey(movieTitle))
                        {
                            schedule[movieTitle] = new List<string>();
                        }
                        schedule[movieTitle].Add(rawTime);
                    }

                    if (schedule.Count == 0)
                    {
                        return $"Lịch chiếu ngày {date:dd/MM/yyyy}: Không có lịch chiếu.";
                    }

                    List<string> formattedSchedule = schedule.Select(s => $"{s.Key}\n {string.Join(", ", s.Value)}\n").ToList();
                    return $"Ngày: {date:dd/MM/yyyy}\n\n" + string.Join("\n", formattedSchedule);
                }
            }
        }
    }

    //private async Task<string> GetTicketPrice()
    //{
    //    using (SqlConnection conn = new SqlConnection(connectionString))
    //    {
    //        await conn.OpenAsync();
    //        SqlCommand cmd = new SqlCommand("SELECT * FROM TicketPrices", conn);
    //        SqlDataReader reader = await cmd.ExecuteReaderAsync();
    //        List<string> prices = new List<string>();
    //        while (await reader.ReadAsync())
    //        {
    //            prices.Add(reader["Type"].ToString() + ": " + reader["Price"].ToString() + " VND");
    //        }
    //        return string.Join(", ", prices);
    //    }
    //}

    //private async Task<string> GetMovies(string type)
    //{
    //    using (SqlConnection conn = new SqlConnection(connectionString))
    //    {
    //        await conn.OpenAsync();
    //        SqlCommand cmd = new SqlCommand("SELECT Title FROM Movies WHERE Status = @Status", conn);
    //        cmd.Parameters.AddWithValue("@Status", type);
    //        SqlDataReader reader = await cmd.ExecuteReaderAsync();
    //        List<string> movies = new List<string>();
    //        while (await reader.ReadAsync())
    //        {
    //            movies.Add(reader["Title"].ToString());
    //        }
    //        return string.Join(", ", movies);
    //    }
    //}

    //private async Task<string> GetTopMovies()
    //{
    //    using (SqlConnection conn = new SqlConnection(connectionString))
    //    {
    //        await conn.OpenAsync();
    //        SqlCommand cmd = new SqlCommand("SELECT Title FROM Movies ORDER BY Rating DESC LIMIT 5", conn);
    //        SqlDataReader reader = await cmd.ExecuteReaderAsync();
    //        List<string> movies = new List<string>();
    //        while (await reader.ReadAsync())
    //        {
    //            movies.Add(reader["Title"].ToString());
    //        }
    //        return string.Join(", ", movies);
    //    }
    //}

    //private async Task<string> GetShowtimes()
    //{
    //    using (SqlConnection conn = new SqlConnection(connectionString))
    //    {
    //        await conn.OpenAsync();
    //        SqlCommand cmd = new SqlCommand("SELECT DISTINCT Time FROM Showtimes", conn);
    //        SqlDataReader reader = await cmd.ExecuteReaderAsync();
    //        List<string> times = new List<string>();
    //        while (await reader.ReadAsync())
    //        {
    //            times.Add(reader["Time"].ToString());
    //        }
    //        return string.Join(", ", times);
    //    }
    //}

    private async Task<string> GetMovieTrailer(string query)
    {
        return "Bạn có thể xem trailer tại: https://yourwebsite.com/trailer";
    }

    private async Task<string> GetMovieDetails(string query)
    {
        return "Thông tin chi tiết tại: https://yourwebsite.com/movie-details";
    }
}

public class ChatRequest
{
    public string Query { get; set; }
}
