using System.Threading.Tasks;
using Action.Common.Events;
using System;

namespace Action.Api.Handlers {
    public class ActivityCreatedHandler : IEventHandler<ActivityCreated> {

        public async Task HandleAsync(ActivityCreated @event) {
            await Task.CompletedTask;
            Console.WriteLine($"Activity Created: {@event.Description}");
        }

    }
}