using CoreText;

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

    public class TextQuestion(string text) : Question(text)
    {
    }
    public class SliderQuestion(string text, int min, int max) : Question(text)
    {
        public int Min { get; } = min;
        public int Max { get; } = max;
    }
    public class RadioQuestion(String text, List<string> options) : Question(text)
    {
        public List<string> Options { get; set; } = options;
    }
    public class QuestionRepository
    {
        private List<Question> data;

    }
}
