using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasketAPI.Data;
using BasketAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketRepo _repo;

        public BasketsController(IBasketRepo repo)
        {
            _repo = repo;
        }

        [HttpGet("{id}", Name="GetBasketById")]
        public ActionResult<Basket> GetBasketById(string id)
        {
            var basket = _repo.GetBasketById(id);

            if (basket != null)
                return Ok(basket);

            return NotFound();
        }

        [HttpPost]
        public ActionResult<Basket> CreatePlatform(Basket basket)
        {
            _repo.CreateBasket(basket);

            return CreatedAtRoute(nameof(GetBasketById), new {Id = basket.Id}, basket);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Basket>> GetAllBaskets()
        {
            return Ok(_repo.GetAllBaskets());
        }

        [HttpDelete]
        public ActionResult DeleteBasketById(string id)
        {
            _repo.DeleteBasketById(id);

            return Ok();
        }
    }
}