using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using ShoppingCart.Repositories;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    public class BasketController : Controller
    {
        private readonly BasketRepository _repository;

        public BasketController(BasketRepository repository)
        {
            _repository = repository;
        }  

        [HttpPost]
        public async Task<IActionResult> AddItems(long userId, [FromBody]IEnumerable<BasketItemDto> items)
        {
            var basket = await _repository.AddItemsToBasket(userId, items);
            return new ObjectResult(basket);
        }

        [HttpGet]
        public async Task<IActionResult> Get(long userId)
        {
            var basket = await _repository.Get(userId);
            return new ObjectResult(basket);
        }
    }
}