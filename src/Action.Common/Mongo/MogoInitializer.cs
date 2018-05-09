using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Action.Common.Mongo
{
    public class MongoInitializer : IDatabaseInitializer
    {
        private bool _initialized;
        private readonly IMongoDatabase _database;
        private readonly IDatabaseSeeder _seeder;
        private readonly bool _seed;

        public MongoInitializer(IMongoDatabase database, IDatabaseSeeder seeder, IOptions<MongoOptions> options)
        {

        }
        public async Task InitializeAsync()
        {
            if(_initialized)
            {
                return;
            }

            RegisterConventions();

            _initialized = true;
            if(!_seed)
            {
                return;
            }

            await _seeder.SeedAsync();
        }

        private void RegisterConventions()
        {
            ConventionRegistry.Register("ActionConventions", new MongoConvention(), x => true);
        }

        private class MongoConvention : IConventionPack
        {
            public IEnumerable<IConvention> Conventions => new List<IConvention>()
            {
                new IgnoreExtraElementsConvention(true),
                new EnumRepresentationConvention(MongoDB.Bson.BsonType.String),
                new CamelCaseElementNameConvention()
            };
        }
    }
}
