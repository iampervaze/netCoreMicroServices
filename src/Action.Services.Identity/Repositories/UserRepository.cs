using Action.Services.Identity.Domain.Models;
using Action.Services.Identity.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Services.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoDatabase _database;

        public UserRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task AddAsync(User user) => await Collection.InsertOneAsync(user);

        public async Task<User> GetAsync(Guid id) => await Collection.AsQueryable().FirstOrDefaultAsync(o => o.Id == id);

        public async Task<User> GetAsync(string email) => await Collection.AsQueryable().FirstOrDefaultAsync(o => o.Email == email.ToLowerInvariant());

        private IMongoCollection<User> Collection => _database.GetCollection<User>("Users");
    }
}