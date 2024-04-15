using Moderation.Entities;
using Moderation.GroupEntryForm;
using Moderation.GroupRulesView;
using Moderation.Repository;

namespace Moderation.Model
{
    class Group(string name, string description, User creator) : IHasID
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public User Creator { get; } = creator;
        public Repository<Question> GroupEntryQuestions { get; } = new();
        public Repository<Rule> GroupRules { get; } = new();
        public Repository<Role> Reports { get; } = new();
        public UserRepository GroupMembers { get; } = new(); // TODO: Poate tine minte si care useri sunt banned/muted?
        // TODO: Report repo dupa ce e definita clasa Report
        
    }
}
