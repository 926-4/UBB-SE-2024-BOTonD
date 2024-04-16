using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Entities.Report;
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
        public Repository<Question> GroupEntryQuestions { get; } = new(); //TODO: QuestionRepository: Repository<Question>
        public Repository<Rule> GroupRules { get; } = new(); //TODO: RuleRepository: Repository<Rule>
        public Repository<Role> Roles { get; } = new(); //TODO: RoleRepository: Repository<Role>
        public Repository<IReport> Reports { get; } = new(); // TODO: ReportRepositoru: Repository<Report>
        public UserRepository GroupMembers { get; } = new(); // TODO: Poate tine minte si care useri sunt banned/muted?
        // TODO: Report repo dupa ce e definita clasa Report
        
    }
}
