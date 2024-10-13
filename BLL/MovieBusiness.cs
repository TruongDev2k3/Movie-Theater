using MODEL;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using DAL;
using System;
namespace BLL
{
    public class MovieBusiness : IMovieBusiness
    {
        private readonly IConfiguration _configuration;

        private MovieRepository _res; // Không cần khởi tạo ở đây

        public MovieBusiness(IConfiguration config)
        {
            _configuration = config;
            _res = new MovieRepository(_configuration); // Khởi tạo _res sau khi _configuration đã được gán giá trị
        }
        public List<MovieModel> GetMovie()
        {
            return _res.GetMovie();
        }
        public List<MovieModel> GetTopMovie()
        {
            return _res.GetTopMovie();
        }
        public List<MovieModel> GetMoviesNowShowing()
        {
            return _res.GetMoviesNowShowing();
        }
        public List<MovieModel> GetUpcomingMovies()
        {
            return _res.GetUpcomingMovies();
        }
        public List<DoanhThuModel> GetProceeds()
        {
            return _res.GetProceeds();
        }
        public List<Data_TicketModel> getDataTicket()
        {
            return _res.getDataTicket();
        }
        public MovieModel GetMovieById(int id)
        {
            return _res.GetMoviebyID(id);
        }
        public MovieModel GetTrailerById(int id)
        {
            return _res.GetTrailerbyID(id);
        }
        public List<FilmAndShowTimeModel> GetShowtimesByDate(string date)
        {
            return _res.GetShowtimesByDate(date);
        }
        public List<DayshowModel> GetMovieShowDays(int movieId)
        {
            return _res.GetMovieShowDays(movieId);
        }
        public List<PremiereModel> GetShowtimesByMovieAndDate(int movieId, DateTime dayShowtime)
        {
            return _res.GetShowtimesByMovieAndDate(movieId, dayShowtime);
        }
        public bool OrderTicket(TicketModel model)
        {
            return _res.OrderTicket(model);
        }
        public bool CreateMovie(MovieModel model)
        {
            return _res.CreateMovie(model);
        }
        public bool UpdateMovie(MovieModel model)
        {
            return _res.UpdateMovie(model);
        }
        public bool DeleteMovie(int movieId)
        {
            return _res.DeleteMovie(movieId);
        }
    }
}
