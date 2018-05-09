﻿using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Services.Activities.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoDatabase _database;
        public CategoryRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task AddAsync(Category category) => await Collection.InsertOneAsync(category);

        public async Task<IEnumerable<Category>> BrowseAsync() =>
            await Collection
            .AsQueryable()
            .ToListAsync();

        public async Task<Category> GetAsync(string name) => await Collection.AsQueryable().FirstOrDefaultAsync(o => o.Name == name.ToLowerInvariant());

        private IMongoCollection<Category> Collection => _database.GetCollection<Category>("Categories");
    }
}
