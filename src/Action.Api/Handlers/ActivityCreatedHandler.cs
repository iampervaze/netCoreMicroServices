using Action.Api.Models;
using Action.Api.Repositories;
using Action.Common.Events;
using System;
using System.Threading.Tasks;

namespace Action.Api.Handlers
{
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated>
    {
        private readonly IActivityRepository _activityRepository;

        public ActivityCreatedHandler(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public async Task HandleAsync(ActivityCreated @event)
        {
            await _activityRepository.AddAsync(new Activity()
            {
                Id = @event.Id,
                UserId = @event.UserId,
                Name = @event.Name,
                Description = @event.Description,
                Category = @event.Category
            });
            Console.WriteLine($"Activity Created: {@event.Name}");
        }
    }
}