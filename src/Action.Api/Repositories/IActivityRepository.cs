using Action.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Action.Api.Repositories
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity model);

        Task<Activity> GetAsync(Guid id);

        Task<IEnumerable<Activity>> BrowseAsync(Guid userId);
    }
}