using Action.Common.Events;
using System;
using System.Threading.Tasks;

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