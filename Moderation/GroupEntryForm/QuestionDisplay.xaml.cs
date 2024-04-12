
namespace Moderation.GroupEntryForm;

public interface IQuestionDisplay
{
    Layout Content();
    string GetResponse();
}
public class TextQuestionDisplay : IQuestionDisplay
{
    private readonly TextQuestion question;
    private Layout content;
    public Layout Content()
    {
        return content;
    }
    public TextQuestionDisplay(TextQuestion question)
    {
        this.question = question;
        CreateControl();
    }

    private void CreateControl()
    {
        var scrollView = new ScrollView
        {
            Content = new Editor()
        };

        var QuestionControl = new StackLayout();
        var QuestionText = new Label { Text = question.Text };
        content.Children.Add(QuestionText);
        content.Children.Add(scrollView);
    }

    public string GetResponse()
    {
        return "";
    }
}

public class SliderQuestionDisplay : IQuestionDisplay
{
    private readonly SliderQuestion question;
    public Layout content;

    public SliderQuestionDisplay(SliderQuestion question)
    {
        this.question = question;
        CreateControl();
    }

    private void CreateControl()
    {
        var slider = new Slider
        {
            Minimum = question.Min,
            Maximum = question.Max
        };

        var minValueLabel = new Label { Text = question.Min.ToString(), HorizontalOptions = LayoutOptions.Start };
        var maxValueLabel = new Label { Text = question.Max.ToString(), HorizontalOptions = LayoutOptions.End };

        var grid = new Grid();
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        grid.Children.Add(minValueLabel);
        grid.Children.Add(slider);
        grid.Children.Add(maxValueLabel);

        Grid.SetColumn(minValueLabel, 0);
        Grid.SetColumn(slider, 1);
        Grid.SetColumn(maxValueLabel, 2);

        var QuestionControl = new StackLayout();
        var QuestionText = new Label { Text = question.Text };
        QuestionControl.Children.Add(QuestionText);
        QuestionControl.Children.Add(grid);
        this.content = QuestionControl;
    }

    public string GetResponse()
    {
        return "";
    }

    public Layout Content()
    {
        return content;
    }
}
