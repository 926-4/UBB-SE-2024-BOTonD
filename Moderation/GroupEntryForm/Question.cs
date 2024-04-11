using CoreText;
using System.Collections;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

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
        private readonly Dictionary<Guid, Question> data = [];
        public Question Get(Guid id)
        {
            return data[id];
        }
        public Collection<Question> GetAll()
        {
            return new Collection<Question>([.. data.Values]);
        }
        public bool Add(Question question)
        {
            return data.TryAdd(question.Id, question);
        }
        public bool Edit(Guid id, Question question)
        {
            if (!data.ContainsKey(id))
                return false;
            data[id] = question;
            return true;
        }
        public bool Delete(Guid id)
        {
            return data.Remove(id);
        }
    }
    public partial class QuestionFormPage : ContentPage
    {
        private QuestionRepository questionsRepo;

        public QuestionFormPage(List<Question> questions)
        {
            InitializeComponent();
            questionsRepo = new();
            foreach(var question in questions)
            {
                questionsRepo.Add(question);
            }
            CreateForm();
        }

        private void CreateForm()
        {
            foreach (var question in questionsRepo.GetAll())
            {
                switch (question.GetType())
                {
                    case Type t when t == typeof(TextQuestion):
                        var textEntry = new Entry();
                        textEntry.Placeholder = question.Text;
                        //stackLayout.Children.Add(textEntry);
                        break;
                    case Type t when t == typeof(Slider):
                        var slider = new Slider();
                        slider.Minimum = 0;
                        slider.Maximum = 100;
                        //stackLayout.Children.Add(slider);
                        break;
                    default:
                        throw new Exception("Invalid question type");
                }
            }
        }
    }
}
