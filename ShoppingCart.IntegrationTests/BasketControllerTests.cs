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
            var response = await _client.PostAsync("/api/basket?userId=1&itemId=1", null);
            response.EnsureSuccessStatusCode();
        }
    }
}
