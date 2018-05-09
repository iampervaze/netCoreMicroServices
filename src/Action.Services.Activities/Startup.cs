using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Common.Commands;
using Action.Common.Mongo;
using Action.Common.RabbitMq;
using Action.Services.Activities.Domain.Repositories;
using Action.Services.Activities.Handlers;
using Action.Services.Activities.Repositories;
using Action.Services.Activities.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Action.Services.Activities
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
            services.AddRabbitMq(Configuration);
            services.AddSingleton<IActivityRepository, ActivityRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddSingleton<IDatabaseSeeder, CustomMongoSeeder>();
            services.AddSingleton<IActivityService, ActivityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ApplicationServices.GetService<IDatabaseInitializer>().InitializeAsync();

            app.UseMvc();
        }
    }
}
