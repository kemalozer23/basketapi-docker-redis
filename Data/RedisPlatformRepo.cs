using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using BasketAPI.Models;
using StackExchange.Redis;

namespace BasketAPI.Data
{
    public class RedisPlatformRepo : IBasketRepo
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisPlatformRepo(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        public void CreateBasket(Basket basket)
        {
            if (basket == null)
                throw new ArgumentOutOfRangeException(nameof(basket));

            var db = _redis.GetDatabase();

            var serialBasket = JsonSerializer.Serialize(basket);

            // db.StringSet(basket.Id, serialBasket);
            // db.SetAdd("BasketSet", serialBasket);

            db.HashSet("hashbasket", new HashEntry[] {new HashEntry(basket.Id, serialBasket)});
        }

        public IEnumerable<Basket?>? GetAllBaskets()
        {
            var db = _redis.GetDatabase();

            // var completeSet = db.SetMembers("BasketSet");

            var completeHash = db.HashGetAll("hashbasket");

            if (completeHash.Length > 0)
                return Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Basket>(val.Value)).ToList();

            return null;
        }

        public Basket? GetBasketByUserId(string userId)
        {
            var db = _redis.GetDatabase();

            // var basket = db.StringGet(id);

            var basket = db.HashGet("hashbasket", userId);

            if (!string.IsNullOrEmpty(basket))
                return JsonSerializer.Deserialize<Basket>(basket);

            return null;
        }

        public void DeleteBasketByUserId(string userId)
        {
            var db = _redis.GetDatabase();

            db.HashDelete("hashbasket", userId);
        }
    }
}