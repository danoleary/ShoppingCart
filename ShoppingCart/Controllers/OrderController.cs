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
    public class OrderController : Controller
    {
        private readonly OrderRepository _repository;

        public OrderController(OrderRepository repository)
        {
            _repository = repository;
        }  

        [HttpPost]
        public async Task<IActionResult> CreateOrder(long userId)
        {
            var order = await _repository.Add(userId);
            return new ObjectResult(order);
        }

    }
}