using System;
using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Events;
using Action.Common.Exceptions;
using Action.Services.Identity.Services;
using RawRabbit;

namespace Action.Services.Identity
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IBusClient _bus;
        private readonly IUserService _userService;
        public CreateUserHandler(IBusClient bus, IUserService userService)
        {
            _bus = bus;
            _userService = userService;
        }
        public async Task HandleAsync(CreateUser command)
        {
            try
            {
                await _userService.RegisterAsync(command.Email, command.Password, command.Username);
                var userCreated = new UserCreated(command.Email, command.Username);
                await _bus.PublishAsync(userCreated);
            }
            catch (ActionException ex)
            {
                await _bus.PublishAsync(new CreateUserRejected(command.Email, ex.Code, ex.Message));
            }

            catch (Exception ex)
            {
                await _bus.PublishAsync(new CreateUserRejected(command.Email, "error", ex.Message));
            }
        }
    }
}