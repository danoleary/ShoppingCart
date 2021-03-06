using System;
using Xunit;
using ShoppingCart;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using ShoppingCart.Models;
using Newtonsoft.Json;
using System.Text;
using ShoppingCart.Repositories;
using System.Collections.Generic;

namespace ShoppingCart.IntegrationTests
{
    public class BasketControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BasketControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task item_can_be_added_to_basket()
        {
            // Act
            var basketItems = new List<BasketItemDto>{
                new BasketItemDto{
                    ItemId = 1,
                    Quantity = 1
                }
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(basketItems),
                                    Encoding.UTF8, 
                                    "application/json");
            var response = await _client.PostAsync("/api/basket?userId=1", stringContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
