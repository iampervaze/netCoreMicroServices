using Action.Services.Identity.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Action.Services.Identity.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);

        Task<User> GetAsync(string email);

        Task AddAsync(User user);
    }
}