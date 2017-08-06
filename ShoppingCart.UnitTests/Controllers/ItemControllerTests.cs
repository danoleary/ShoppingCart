using System;
using Xunit;
using ShoppingCart.Repositories;
using ShoppingCart.Controllers;
using ShoppingCart.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ShoppingCart.UnitTests
{
    public class ItemControllerTests
    {   
        [Fact]
        public async Task get_returns_a_list_of_items()
        {
            // Arrange
            var mockRepo = new Mock<ItemRepository>();
            var controller = new ItemController(mockRepo.Object);


            // Act
            var result = await controller.GetAll();

            // Assert
            Assert.IsType<List<Item>>(result);
        }
    }

    
}
