using Moderation.Repository;

namespace Moderation.GroupRulesView
{
    public class Rule : IIDInterface
    {
        public string Text { get; }
        public Guid ID { get; set; } = Guid.NewGuid();

        public Rule(string text)
        {
            Text = text;
        }
    }
}
