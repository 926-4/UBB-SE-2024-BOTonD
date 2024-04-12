namespace Moderation.GroupEntryForm
{
    public class RadioQuestion(String text, List<string> options) : Question(text)
    {
        public List<string> Options { get; set; } = options;
    }
}
