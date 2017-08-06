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
    public class ItemRepositoryTests
    {   
        private ItemRepository _testClass;

        public ItemRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<ShoppingCartContext>()
                      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                      .Options;
            var context = new ShoppingCartContext(options);
            this._testClass = new ItemRepository(context);
            var importService = new ItemImportService(context);
            importService.ImportItems().Wait();
        }

        [Fact]
        public async Task when_getting_all_items__Then_all_items_are_returned()
        {
            //act
            var result = await this._testClass.GetAll();

            //assert
            Assert.Equal(5, result.Count());
            // test the values are correct etc
        }
    }
}
