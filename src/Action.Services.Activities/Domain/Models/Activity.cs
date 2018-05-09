﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            Id = id;
            Category = category.Name;
            Name = name;
            UserId = userId;
            Description = description;
            CreatedAt = createdAt;
        }

    }
}
