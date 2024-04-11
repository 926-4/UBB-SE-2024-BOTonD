namespace Moderation.GroupEntryForm
{
    public interface IQuestion
    {
        string Text { set;  get; }
    
    }
    public class TextQuestion(String text) : IQuestion
    {
        public string Text { get; set; } = text;
    }
    public class SliderQuestion(String text, int min, int max) : IQuestion
    {

        public int Min { get; set; } = min;
        public int Max { get; set; } = max;
        public string Text { get; set; } = text;
    }
    public class RadioQuestion(String text, List<string> options) : IQuestion
    {

        public List<string> Options { get; set; } = options;
        public string Text { get; set; } = text;
    }
}
