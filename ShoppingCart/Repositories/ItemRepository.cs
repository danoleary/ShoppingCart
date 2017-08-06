using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace ShoppingCart.Repositories
{
    public class ItemRepository
    {
        private readonly ShoppingCartContext _context;

        public ItemRepository(){} // for moq

        public ItemRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<Item>> GetAll()
        {
            return await _context.Items.ToListAsync();
        } 

    }
}