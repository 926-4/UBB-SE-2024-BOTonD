using Moderation.Model;
using Moderation.Repository;

namespace Moderation.GroupRulesView
{
    public class Rule(string text) : IHasID
    {
        public string Text { get; } = text;
        public Guid Id { get; } = Guid.NewGuid();
    }
}
