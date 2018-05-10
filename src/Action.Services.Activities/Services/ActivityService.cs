using System;
using System.Threading.Tasks;
using Action.Common.Exceptions;
using Action.Services.Activities.Domain.Models;
using Action.Services.Activities.Domain.Repositories;

namespace Action.Services.Activities.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ActivityService(IActivityRepository activityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task AddAsync(Guid id, Guid userId, string categoryName, string name, string description, DateTime createdAt)
        {
            var activityCategory = await _categoryRepository.GetAsync(categoryName);
            if (activityCategory == null)
            {
                throw new ActionException("category_not_found", $"Category: {categoryName} was not found");
            }
            var activity = new Activity(id, activityCategory, userId, name, description, createdAt);
            await _activityRepository.AddAsync(activity);
        }
    }
}