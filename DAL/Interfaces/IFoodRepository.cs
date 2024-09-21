using System;
using System.Collections.Generic;
using System.Text;
using MODEL;
namespace DAL.Interfaces
{
    public partial interface IFoodRepository
    {
        List<FoodModel> GetFood();
        FoodModel GetFoodbyID(int id);
    }
}

