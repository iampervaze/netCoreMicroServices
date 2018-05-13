using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Action.Services.Activities.Tests.Integration.Controllers
{
    public class HomeControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HomeControllerTests()
        {
            var builder = WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Development");
            _server = new TestServer(builder);
            _client = _server.CreateClient();
        }

        [Fact]
        public async Task home_controller_get_should_return_string_content()
        {
            var response = await _client.GetAsync("/");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            content.Should().BeEquivalentTo("Hello From Action API");
        }
    }
}