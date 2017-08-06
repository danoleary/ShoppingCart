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
    public class ItemController : Controller
    {
        private readonly ItemRepository _repository;

        public ItemController(ItemRepository repository)
        {
            _repository = repository;
        }  

        [HttpGet]
        public async Task<IEnumerable<Item>> GetAll()
        {
            return await _repository.GetAll();
        }
    }
}