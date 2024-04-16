using Moderation.Model;
using Moderation.Repository;

namespace Moderation.GroupRulesView
{
    public class Rule(string text,Guid GroupId) : IHasID
    {
        public string Text { get; } = text;
        public Guid Id { get; } = Guid.NewGuid();

        public Guid GroupId { get; set; } = GroupId;
    }
}
