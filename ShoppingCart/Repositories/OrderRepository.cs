using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;

namespace ShoppingCart.Repositories
{
    public class OrderRepository
    {
        private readonly ShoppingCartContext _context;

        public OrderRepository() { } // for moq

        public OrderRepository(ShoppingCartContext context)
        {
            _context = context;
        }

        public virtual async Task<Invoice> Add(long userId)
        {
            var user =
                await _context.Users
                        .Include(x => x.Basket.Items)
                        .ThenInclude(y => y.Item)
                        .SingleOrDefaultAsync(x => x.Id == userId);
            foreach (var item in user.Basket.Items)
            {
                item.Item.Stock = item.Item.Stock - item.Quantity;
                if(item.Item.Stock < 0) throw new Exception(); // handle this properly
            }
            var order = new Order
            {
                User = user,
                Items = user.Basket.Items.Select(x => new OrderItem
                {
                    Item = x.Item,
                    Quantity = x.Quantity
                }).ToList()
            };
            _context.Orders.Add(order);
            user.Basket.Items = new List<BasketItem>();
            await _context.SaveChangesAsync();
            return new Invoice
            {
                Lines = order.Items.Select(x => new InvoiceLine{
                    ItemId = x.Item.Id,
                    Name = x.Item.Name,
                    Price = x.Item.Price,
                    Quantity = x.Quantity
                }),
                Total = order.Items.Select(x => x.Item.Price).Sum()
            };
        }

    }

    public class Invoice
    {
        public IEnumerable<InvoiceLine> Lines {get; set;}
        public Decimal Total {get; set;}
    }

    public class InvoiceLine
    {
        public long ItemId {get; set;}
        public string Name {get; set;}
        public Decimal Price {get; set;}
        public int Quantity {get; set; }
    }
}