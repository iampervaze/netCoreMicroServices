using System;
using System.Threading.Tasks;
using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;
using Action.Services.Activities.Services;
using Moq;
using Xunit;

namespace Action.Services.Activities.Tests.Unit.Services
{
    public class ActivityServiceTests
    {
        [Fact]
        public async Task activity_service_add_async_should_succeed()
        {
            var activityRepositoryMocked = new Mock<IActivityRepository>();
            var categoryRepositoryMocked = new Mock<ICategoryRepository>();

            var category = "test";

            categoryRepositoryMocked.Setup(x => x.GetAsync(category)).ReturnsAsync(new Category(category));

            var activityService = new ActivityService(activityRepositoryMocked.Object, categoryRepositoryMocked.Object);

            var id = Guid.NewGuid();
            await activityService.AddAsync(id, Guid.NewGuid(), category, "name", "desc", DateTime.Now);

            categoryRepositoryMocked.Verify(x => x.GetAsync(category), Times.Once);

            activityRepositoryMocked.Verify(x => x.AddAsync(It.IsAny<Activity>()), Times.Once);
        }
    }
}