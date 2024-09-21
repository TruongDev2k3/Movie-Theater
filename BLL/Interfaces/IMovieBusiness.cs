
using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BTL_NguyenVanTruong_.BLL.Interfaces
{
    public partial interface IMovieBusiness
    {
        //bool CreateAccount(AccountModel model);
        //bool UpdateAccount(AccountModel model);
        bool OrderTicket(TicketModel ticket);
        MovieModel GetMovieById(int id);
        MovieModel GetTrailerById(int id);
        //bool DeleteAccount(int mtk);
        List<MovieModel> GetMovie();
        List<FilmAndShowTimeModel> GetShowtimesByDate(string date);
        List<DayshowModel> GetMovieShowDays(int movieId);
        List<PremiereModel> GetShowtimesByMovieAndDate(int movieId, DateTime dayShowtime);
        //List<AccountModel> SearchAccount(string tk);
    }
}
