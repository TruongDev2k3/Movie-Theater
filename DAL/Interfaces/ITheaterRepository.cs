using MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
   
    public partial interface ITheaterRepository
    {
        bool CreateTheater(TheaterModel model);
        bool UpdateTheater(TheaterModel model);
        TheaterModel GetTheaterbyID(int mtl);
        bool DeleteTheater(int mtl);
        List<TheaterModel> GetTheater();
    }
}
