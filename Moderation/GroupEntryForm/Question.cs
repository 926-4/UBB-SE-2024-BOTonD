using Moderation.Repository;

namespace Moderation.GroupEntryForm
{
    public abstract class Question(string text) : IHasID
    {
        public string Text { get; } = text;
        public Guid Id { get; } = Guid.NewGuid();
    }
}
