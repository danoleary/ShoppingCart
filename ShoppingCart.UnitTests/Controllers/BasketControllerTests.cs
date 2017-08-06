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
    public class BasketControllerTests
    {   
        [Fact]
        public async Task add_item_returns_an_object_result()
        {
            // Arrange
            var mockRepo = new Mock<BasketRepository>();
            mockRepo.Setup(m =>
                m.AddItemToBasket(It.IsAny<long>(), It.IsAny<long>())).Returns(Task.FromResult(It.IsAny<Basket>()));
            var controller = new BasketController(mockRepo.Object);


            // Act
            var result = await controller.AddItem(It.IsAny<long>(), It.IsAny<long>());

            // Assert
            Assert.IsType<ObjectResult>(result);
        }
    }

    
}
