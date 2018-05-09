using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Action.Services.Activities.Services
{
    public interface IActivityService
    {
        Task AddAsync(Guid id, Guid userId, string categoryName, string name, string description, DateTime createdAt);
    }
}
