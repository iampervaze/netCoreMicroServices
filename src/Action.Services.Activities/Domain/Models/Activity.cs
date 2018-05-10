using System;
using Action.Common.Exceptions;

namespace Action.Services.Activities.Domain.Models
{
    public class Activity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        protected Activity()
        {
        }

        public Activity(Guid id, Category category, Guid userId, string name, string description, DateTime createdAt)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ActionException("empty_activity_name", "Activity name cannot be empty");

            Id = id;
            Category = category.Name;
            Name = name;
            UserId = userId;
            Description = description;
            CreatedAt = createdAt;
        }
    }
}