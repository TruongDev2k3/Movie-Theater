﻿using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BTL_NguyenVanTruong_.DAL.Interfaces
{
    public partial interface IMovieRepository
    {
        
        bool OrderTicket(TicketModel ticket);

        MovieModel GetMoviebyID(int id);
        MovieModel GetTrailerbyID(int id);
        List<CommentModel> getCommentByMovieID(int movieId);
        List<MovieModel> GetMovie();
        List<FilmAndShowTimeModel> GetShowtimesByDate(string date);
        List<DayshowModel> GetMovieShowDays(int movieId);
        List<PremiereModel> GetShowtimesByMovieAndDate(int movieId, DateTime dayShowtime);
        bool CreateMovie(MovieModel model);
        bool UpdateMovie(MovieModel model);
        bool DeleteMovie(int movieId);
        List<MovieModel> GetTopMovie();
        List<MovieModel> GetMoviesNowShowing();
        List<MovieModel> GetUpcomingMovies();
        List<DoanhThuModel> GetProceeds();
        List<Data_TicketModel> getDataTicket();
        bool AddComment(CommentModel model);

    }
}
