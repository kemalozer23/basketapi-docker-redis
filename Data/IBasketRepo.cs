using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketAPI.Models;

namespace BasketAPI.Data
{
    public interface IBasketRepo
    {
        void CreateBasket(Basket basket);
        Basket? GetBasketByUserId(string userId);
        IEnumerable<Basket?>? GetAllBaskets();

        void DeleteBasketByUserId(string userId);
    }
}