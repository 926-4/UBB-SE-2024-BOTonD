using Moderation.Entities;
using Moderation.Repository;

namespace Moderation.Model
{
    public class Rule : IHasID
    {
        public Guid Id { get; }
        public Guid GroupId { get; set; }
        public string Title {  get; set; }
        public string Text { get; }

        public Rule(Guid groupId, string title, string text)
        {
            Id = Guid.NewGuid();
            GroupId = groupId;
            Title = title;
            Text = text;
        }
        public Rule(Guid id, Guid groupId, string title, string text)
        {
            Id = id;
            GroupId = groupId;
            Title = title;
            Text = text;
        }
    }
}