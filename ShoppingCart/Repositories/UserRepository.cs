using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace ShoppingCart.Repositories
{
    public class UserRepository
    {
        private readonly ShoppingCartContext _context;

        public UserRepository(){} // for moq

        public UserRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public virtual async Task<long> Add()
        {
            var newUser = new User{
                Basket = new Basket()
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.Id;
        } 

    }
}