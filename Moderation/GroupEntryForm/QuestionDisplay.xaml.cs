

namespace Moderation.GroupEntryForm;

public abstract partial class QuestionDisplay : ContentView
{
    public abstract string GetQuestion();
    public abstract string GetResponse();
}
public class TextQuestionDisplay : QuestionDisplay
{
    private readonly TextQuestion question;
    public TextQuestionDisplay(TextQuestion question)
    {
        this.question = question;
        Content = new StackLayout();
        CreateContent();
    }

    public override string GetQuestion()
    {
        return question.Text;
    }
    private void CreateContent()
    {
        var QuestionTextLabel = new Label { Text = question.Text };
        var InputArea = new ScrollView { Content = new Editor() , VerticalScrollBarVisibility = ScrollBarVisibility.Always };
        ((StackLayout)Content).Children.Add(QuestionTextLabel);
        ((StackLayout)Content).Children.Add(InputArea);
    }

    public override string GetResponse()
    {
        var BoxWithTextInputIndex = 1;
        var MainComponentLayout = (StackLayout)Content;
        var BoxWithTextInput = (ScrollView)MainComponentLayout.Children[BoxWithTextInputIndex];
        var TextInput = (Editor)BoxWithTextInput.Content;
        return TextInput.Text;
    }
}

public class SliderQuestionDisplay : QuestionDisplay
{
    private readonly SliderQuestion question;
    public SliderQuestionDisplay(SliderQuestion question)
    {
        this.question = question;
        Content = new StackLayout();
        CreateContent();
    }
    public override string GetQuestion()
    {
        return question.Text;
    }

    private void CreateContent()
    {
        var slider = new Slider { Minimum = question.Min, Maximum = question.Max };
        var minValueLabel = new Label { Text = question.Min.ToString(), HorizontalOptions = LayoutOptions.Start };
        var maxValueLabel = new Label { Text = question.Max.ToString(), HorizontalOptions = LayoutOptions.End };

        var grid = new Grid();

        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 5 });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 5 });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        grid.Children.Add(minValueLabel);
        grid.Children.Add(slider);
        grid.Children.Add(maxValueLabel);

        Grid.SetColumn(minValueLabel, 0);
        Grid.SetColumn(slider, 2);
        Grid.SetColumn(maxValueLabel, 4);

        var questionText = new Label { Text = question.Text };

        ((StackLayout)Content).Children.Add(questionText);
        ((StackLayout)Content).Children.Add(grid);
    }

    public override string GetResponse()
    {
        var GridIndexInMainLayout = 1;
        var SliderIndex = 2;
        var MainComponentLayout = (StackLayout)Content;
        var Grid = (Grid)MainComponentLayout.Children[GridIndexInMainLayout];
        var Slider = (Slider)Grid.Children[SliderIndex];

        return Slider.Value.ToString();
    }
}
public class QuestionDisplayFactory 
{
    public static QuestionDisplay GetQuestionDisplay(Question question)
    {
        return question switch
        {
            TextQuestion textQuestion => new TextQuestionDisplay(textQuestion),
            SliderQuestion sliderQuestion => new SliderQuestionDisplay(sliderQuestion),
            _ => throw new NotSupportedException("Question type not supported."),
        };
    }
}
