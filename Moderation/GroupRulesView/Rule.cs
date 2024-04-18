using Moderation.Model;
using Moderation.Repository;

namespace Moderation.GroupRulesView
{
    public class Rule : IHasID
    {
        public string Text { get; }
        public Guid Id { get; } = Guid.NewGuid();
        public Guid GroupId { get; set; }

        public Rule(string text)
        {
            this.Text = text;
        }
        
        public Rule(string text, Guid groupId)
        { 
            this.Text = text;
            this.GroupId = groupId;
        }
    }
}