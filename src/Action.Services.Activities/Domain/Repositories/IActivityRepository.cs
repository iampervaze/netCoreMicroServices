using Action.Services.Activities.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Action.Services.Activities.Domain.Repositories
{
    public interface IActivityRepository
    {
        Task<Activity> GetAsync(Guid id);

        Task<IEnumerable<Activity>> BrowseAsync();

        Task AddAsync(Activity activity);
    }
}