using System;
using Xunit;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using ShoppingCart.Services;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace ShoppingCart.UnitTests
{
    public class BasketRepositoryTests
    {   
        private ShoppingCartContext _context;
        private BasketRepository _testClass;

        public BasketRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ShoppingCartContext>()
                      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                      .Options;
            _context = new ShoppingCartContext(options);
            _testClass = new BasketRepository(_context);
        }

        [Fact]
        public async Task when_an_item_is_in_stock__Then_it_can_be_added_to_a_users_basket()
        {
            //arrange
            var user = new User{
                Basket = new Basket()
            };
            var item = new Item{
                Name = "a name",
                Description = "a descirption",
                Stock = 1,
                Price = 10.0M
            };
            _context.Users.Add(user);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            //act
            await this._testClass.AddItemToBasket(user.Id, item.Id);

            //assert
            user = 
                await _context.Users
                        .Include(x => x.Basket)
                        .SingleAsync(x => x.Id == user.Id);
            Assert.Equal(item.Id, user.Basket.Items.Single().Item.Id);
        }

        [Fact]
        public async Task when_an_item_is_not_in_stock_it_is_not_added_to_the_basket()
        {
            //arrange
            var user = new User{
                Basket = new Basket()
            };
            var item = new Item{
                Name = "a name",
                Description = "a descirption",
                Stock = 0,
                Price = 10.0M
            };
            _context.Users.Add(user);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            //act
            await this._testClass.AddItemToBasket(user.Id, item.Id);

            //assert
            user = 
                await _context.Users
                        .Include(x => x.Basket)
                        .SingleAsync(x => x.Id == user.Id);
            Assert.True(!user.Basket.Items.Any());
        }

        [Fact]
        public async Task when_a_request_is_made_to_add_an_item_to_a_basket_and_that_item_is_already_in_the_basket__Then_the_quantity_in_the_basket_is_increased()
        {
            //arrange
            var user = new User{
                Basket = new Basket()
            };
            var item = new Item{
                Name = "a name",
                Description = "a descirption",
                Stock = 2,
                Price = 10.0M
            };
            _context.Users.Add(user);
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
            await this._testClass.AddItemToBasket(user.Id, item.Id);

            //act
            await this._testClass.AddItemToBasket(user.Id, item.Id);

            //assert
            user = 
                await _context.Users
                        .Include(x => x.Basket)
                        .SingleAsync(x => x.Id == user.Id);
            Assert.True(user.Basket.Items.Single().Quantity == 2);
        }

        [Fact]
        public async Task basket_can_be_retrieved_by_user_id()
        {
            //arrange
            var user = new User{
                Basket = new Basket()
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            //act
            var result = await _testClass.Get(user.Id);

            //assert
            Assert.Equal(0, result.Items.Count());
        }
    }
}
