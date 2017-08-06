using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace ShoppingCart.Repositories
{
    public class BasketRepository
    {
        private readonly ShoppingCartContext _context;

        public BasketRepository() { }

        public BasketRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public virtual async Task<Basket> AddItemsToBasket(long userId, IEnumerable<BasketItemDto> items)
        {
            var user =
                await _context.Users
                        .Include(x => x.Basket.Items)
                        .ThenInclude(y => y.Item)
                        .SingleOrDefaultAsync(x => x.Id == userId);
            // handle invalid user id

            foreach (var basketItem in items)
            {
                var item = await _context.Items.SingleOrDefaultAsync(x => x.Id == basketItem.ItemId);
                //handle invalid item id

                var itemInBasket = user.Basket.Items.SingleOrDefault(x => x.Item.Id == basketItem.ItemId);
                if (ItemCanBeAdded(item, basketItem.Quantity, itemInBasket))
                {
                    if (itemInBasket == null)
                    {
                        user.Basket.Items.Add(new BasketItem
                        {
                            Item = item,
                            Quantity = basketItem.Quantity
                        });
                    }
                    else
                    {
                        itemInBasket.Quantity = itemInBasket.Quantity + basketItem.Quantity;
                    }
                }
            }

            await _context.SaveChangesAsync();

            return user.Basket;
        }

        private bool ItemCanBeAdded(Item item, int quantity, BasketItem itemInBasket)
        {
            if (item.Stock == 0) return false;
            if ((itemInBasket?.Quantity ?? 0) + quantity > item.Stock) return false;
            return true;

        }

        public virtual async Task<Basket> Get(long userId)
        {
            var user = await _context.Users.Include(x => x.Basket.Items).SingleOrDefaultAsync(x => x.Id == userId);
            // handle invalid user id

            return user.Basket;
        }
    }

    public class BasketItemDto
    {
        public long ItemId {get; set;}
        public int Quantity {get; set;}
    }
}