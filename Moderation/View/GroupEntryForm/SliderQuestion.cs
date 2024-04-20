namespace Moderation.GroupEntryForm
{
    public class SliderQuestion(string text, int min, int max) : Question(text)
    {
        public int Min { get; } = min;
        public int Max { get; } = max;
    }
}
