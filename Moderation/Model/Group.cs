﻿using Moderation.Entities;
using Moderation.Entities.Report;
using Moderation.GroupEntryForm;
using Moderation.GroupRulesView;
using Moderation.Repository;

namespace Moderation.Model
{
    public class Group : IHasID
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public User Creator { get; }
        public Repository<Question> GroupEntryQuestions { get; }
        public Repository<Rule> GroupRules { get; }
        public Repository<Role> Roles { get; }
        public Repository<PostReport> Reports { get; }
        public Dictionary<User, Role> GroupMembers { get; }
        public Group(string name, string description, User creator)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Creator = creator;
            GroupEntryQuestions = new();
            GroupRules = new();
            Roles = new();
            Reports = new();
            GroupMembers = [];
            var arrayOfAllPermissions = Enum.GetValues(typeof(Permission));
            var listOfAllPermissions = new List<Permission>(arrayOfAllPermissions.Cast<Permission>());
            Role creatorRole = new("Creator", listOfAllPermissions);
            Roles.Add(creatorRole.Id, creatorRole);
            GroupMembers.Add(creator, creatorRole);
        }
        public Group(Guid id, string name, string description, User creator)
        {
            Id = id;
            Name = name;
            Description = description;
            Creator = creator;
            GroupEntryQuestions = new();
            GroupRules = new();
            Roles = new();
            Reports = new();
            GroupMembers = [];
            var arrayOfAllPermissions = Enum.GetValues(typeof(Permission));
            var listOfAllPermissions = new List<Permission>(arrayOfAllPermissions.Cast<Permission>());
            Role creatorRole = new("Creator", listOfAllPermissions);
            Roles.Add(creatorRole.Id, creatorRole);
            GroupMembers.Add(creator, creatorRole);
        }
    }
}