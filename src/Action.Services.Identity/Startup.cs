﻿using Action.Common.Auth;
using Action.Common.Commands;
using Action.Common.Mongo;
using Action.Common.RabbitMq;
using Action.Services.Identity.Domain.Repositories;
using Action.Services.Identity.Domain.Services;
using Action.Services.Identity.Repositories;
using Action.Services.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Action.Services.Identity
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
            services.AddJwt(Configuration);
            services.AddMongoDb(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddSingleton<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddSingleton<IEncrypter, Encrypter>();
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserService, UserService>();
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