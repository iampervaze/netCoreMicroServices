using Action.Common.Commands;
using Action.Common.Events;
using Action.Common.Exceptions;
using Action.Services.Activities.Services;
using RawRabbit;
using System;
using System.Threading.Tasks;

namespace Action.Services.Activities.Handlers
{
    public class CreateActivityHandler : ICommandHandler<CreateActivity>
    {
        private readonly IBusClient _bus;
        private readonly IActivityService _activityService;

        public CreateActivityHandler(IBusClient bus, IActivityService activityService)
        {
            _bus = bus;
            _activityService = activityService;
        }

        public async Task HandleAsync(CreateActivity command)
        {
            try
            {
                await _activityService.AddAsync(command.Id, command.UserId, command.Category, command.Name, command.Description, command.CreatedAt);
                var activityCreated = new ActivityCreated(command.Id, command.UserId, command.Name, command.Category, command.Description, DateTime.Now);
                await _bus.PublishAsync(activityCreated);
            }
            catch (ActionException ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(command.Id, ex.Code, ex.Message));
            }
            catch (Exception ex)
            {
                await _bus.PublishAsync(new CreateActivityRejected(command.Id, "error", ex.Message));
            }
        }
    }
}