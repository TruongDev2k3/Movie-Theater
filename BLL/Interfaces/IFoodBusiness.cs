using System;
using System.Collections.Generic;
using System.Text;
using MODEL;

namespace BLL.Interfaces
{
    public partial interface IFoodBusiness
    {
        List<FoodModel> GetFood();
        FoodModel GetFoodbyID(int id);
    }
}


