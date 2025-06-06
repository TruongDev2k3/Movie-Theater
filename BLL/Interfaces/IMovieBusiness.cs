
using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IMovieBusiness
    {
        
        bool OrderTicket(TicketModel ticket);
        MovieModel GetMovieById(int id);
        MovieModel GetTrailerById(int id);
        bool CreateMovie(MovieModel model);
        bool UpdateMovie(MovieModel model);
        bool DeleteMovie(int movieId);
        List<MovieModel> GetMovie();
        List<FilmAndShowTimeModel> GetShowtimesByDate(string date);
        List<DayshowModel> GetMovieShowDays(int movieId);
        List<PremiereModel> GetShowtimesByMovieAndDate(int movieId, DateTime dayShowtime);
        List<MovieModel> GetTopMovie();
        List<MovieModel> GetMoviesNowShowing();
        List<MovieModel> GetUpcomingMovies();
        List<DoanhThuModel> GetProceeds();
        List<Data_TicketModel> getDataTicket();
        bool AddComment(CommentModel model);
        List<CommentModel> getCommentByMovieID(int movieId);
    }
}
