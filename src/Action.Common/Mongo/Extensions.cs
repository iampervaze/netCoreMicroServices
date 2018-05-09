﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Action.Common.Mongo
{
    public static class Extensions
    {

        public static void AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<MongoOptions>(configuration.GetSection("mongo"));
            services.AddSingleton<MongoClient>(c =>
            {
                var options = c.GetService<IOptions<MongoOptions>>();

                return new MongoClient(options.Value.ConnectionString);
            });

            services.AddSingleton<IMongoDatabase>(c =>
            {
                var client = c.GetService<MongoClient>();
                var options = c.GetService<IOptions<MongoOptions>>();
                return client.GetDatabase(options.Value.Database);
            });

            services.AddSingleton<IDatabaseInitializer, MongoInitializer>();
            services.AddSingleton<IDatabaseSeeder, MongoSeeder>();
        }
    }
}
