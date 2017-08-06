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
    public class UserRepositoryTests
    {   
        private ShoppingCartContext _context;
        private UserRepository _testClass;

        public UserRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ShoppingCartContext>()
                      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                      .Options;
            _context = new ShoppingCartContext(options);
            this._testClass = new UserRepository(_context);
            var importService = new ItemImportService(_context);
            importService.ImportItems().Wait();
        }

        [Fact]
        public async Task when_getting_all_items__Then_all_items_are_returned()
        {
            //act
            var result = await this._testClass.Add();

            //assert
            var user = _context.Users.Single();
            Assert.Equal(user.Id, result);
        }
    }
}
