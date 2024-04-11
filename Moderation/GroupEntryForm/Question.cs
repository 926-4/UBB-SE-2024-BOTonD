namespace Moderation.GroupEntryForm
{
    public interface IQuestion
    {
        string Text { set;  get; }
    
    }
    public class TextQuestion : IQuestion
    {
        public string Text { get; set; }
        public TextQuestion(String text)
        {
            Text = text;
        }
    }
    public class SliderQuestion : IQuestion
    {

        public int Min { get; set; }
        public int Max { get; set; }
        public string Text { get; set; }
        public SliderQuestion(String text, int min, int max) 
        {
            Min = min;
            Max = max;
            Text = text;
        }
    }
    public class RadioQuestion : IQuestion
    {

        public List<string> Options { get; set; }
        public string Text { get; set; }
        public RadioQuestion(String text, List<string> options)
        {
            Text = text;
            Options = options;
        }
    }
}
