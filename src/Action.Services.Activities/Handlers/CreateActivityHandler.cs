using Action.Common.Commands;
using Action.Common.Events;
using RawRabbit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {

        private readonly IBusClient _bus;
        
        public CreateActivityHandler(IBusClient bus)
        {
            _bus = bus;
        }
        public async Task HandleAsync(CreateActivity command)
        {
            await Task.CompletedTask;
            Console.WriteLine($"Create Activity: {command.Name}");
            var activity = new ActivityCreated(command.Id, command.UserId, command.Name, command.Category, command.Description, DateTime.Now);
            await _bus.PublishAsync(activity);
        }
    }
}
