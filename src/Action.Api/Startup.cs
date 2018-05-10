using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Action.Common.RabbitMq;
using Action.Common.Events;
using Action.Common.Mongo;
using Action.Api.Handlers;
using Action.Common.Auth;
using Action.Api.Repositories;

namespace Action.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddMongoDb(Configuration);
            services.AddJwt(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddSingleton<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();
            services.AddSingleton<IEventHandler<UserCreated>, UserCreatedHandler>();
            services.AddSingleton<IActivityRepository, ActivityRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
