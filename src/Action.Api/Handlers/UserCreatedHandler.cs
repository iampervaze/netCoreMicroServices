using System;
using System.Threading.Tasks;
using Action.Common.Events;

namespace Action.Api.Handlers
{
    public class UserCreatedHandler : IEventHandler<UserCreated>
    {
        public async Task HandleAsync(UserCreated @event)
        {
            Console.WriteLine($"User Created: {@event.Email}");
            await Task.CompletedTask;
        }
    }
}