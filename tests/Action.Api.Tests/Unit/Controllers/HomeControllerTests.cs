using Action.Api.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Action.Api.Tests.Unit.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void home_controller_should_return_string_content()
        {
            var controller = new HomeController();
            var result = controller.Get();

            var contentResult = result as ContentResult;
            contentResult.Should().NotBeNull();
            contentResult.Content.ShouldAllBeEquivalentTo("Hello From Action API");
        }
    }
}