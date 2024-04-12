namespace Moderation.GroupEntryForm
{
    public abstract class Question
    {
        public string Text { get; }
        public Guid Id { get; } = Guid.NewGuid();

        public Question(string text)
        {
            Text = text;
        }
    }
}
