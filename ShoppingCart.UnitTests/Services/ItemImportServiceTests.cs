using System;
using Xunit;
using ShoppingCart.Models;
using ShoppingCart.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using ShoppingCart.Services;

namespace ShoppingCart.UnitTests
{
    public class ItemImportServiceTests
    {   
        private ShoppingCartContext _context;
        private ItemImportService _testClass;

        public ItemImportServiceTests()
        {
            var options = new DbContextOptionsBuilder<ShoppingCartContext>()
                      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                      .Options;
            _context = new ShoppingCartContext(options);
            this._testClass = new ItemImportService(_context);
        }

        [Fact]
        public async Task when_the_csv_data_is_imported_it_is_saved_to_the_database()
        {
            //arrange
            await _testClass.ImportItems();

            //assert
            var items = await _context.Items.ToListAsync();
            Assert.Equal(5, items.Count());
        }


    }

    
}
