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
    public class ItenControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public ItenControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task list_of_items_can_be_retrieved_after_startup()
        {
            // Act
            var response = await _client.GetAsync("/api/item");
            response.EnsureSuccessStatusCode();
            // test content
        }
    }
}
