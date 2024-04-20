using Moderation.Entities;
using Moderation.Repository;

namespace Moderation.Model
{
    public class Rule : IHasID
    {
        public string Text { get; }
        public Guid Id { get; } = Guid.NewGuid();
        public Guid GroupId { get; set; }

        public Rule(string text)
        {
            Text = text;
        }

        public Rule(string text, Guid groupId)
        {
            Text = text;
            GroupId = groupId;
        }
    }
}