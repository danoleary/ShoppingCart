using System;
using Xunit;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using ShoppingCart.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace ShoppingCart.UnitTests
{
    public class OrderRepositoryTests
    {   
        private ShoppingCartContext _context;
        private OrderRepository _testClass;

        public OrderRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ShoppingCartContext>()
                      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                      .Options;
            _context = new ShoppingCartContext(options);
            this._testClass = new OrderRepository(_context);
            var importService = new ItemImportService(_context);
            importService.ImportItems().Wait();
        }

        [Fact]
        public async Task when_an_order_is_placed__Then_an_order_is_created_and_the_items_stock_is_updated()
        {
            //arrange
            var user = new User{
                Basket = new Basket{
                    Items = new List<BasketItem>{
                        new BasketItem{
                            Item = new Item{
                                Name = "a name",
                                Description = "a description",
                                Stock = 1,
                                Price = 10.0M
                            },
                            Quantity = 1
                        }
                    }
                }
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //act
            await this._testClass.Add(user.Id);

            //assert
            var order = await _context.Orders.SingleAsync();
            Assert.Equal(1, order.Items.Count());
            var item = await _context.Items.SingleAsync(x => x.Id == order.Items.Single().Item.Id);
            Assert.Equal(0, item.Stock);
            user = await _context.Users.SingleAsync(x => x.Id == user.Id);
            Assert.Equal(0, user.Basket.Items.Count());
        }
    }
}
