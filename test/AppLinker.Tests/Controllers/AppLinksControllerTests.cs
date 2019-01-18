using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AppLinker.Controllers
{
    public class AppLinksControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> factory;

        public AppLinksControllerTests(WebApplicationFactory<Startup> factory) =>
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

        [Fact]
        public async Task GetAppLink()
        {
            // Arrange
            var client = this.factory.CreateClient();

            // Act
            var response = await client.GetAsync("/");
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal("/", content);
        }
    }
}
