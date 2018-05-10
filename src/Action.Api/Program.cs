using Action.Common.Events;
using Action.Common.Services;

namespace Action.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServiceHost.Create<Startup>(args)
                .UseRabbitMq()
                .SubscribeToEvent<ActivityCreated>()
                .SubscribeToEvent<UserCreated>()
                .Build()
                .Run();
        }
    }
}