namespace Moderation.GroupEntryForm;

public partial class GroupEntryForm : ContentPage
{
    private readonly IEnumerable<Question> formQuestions;
    public GroupEntryForm(IEnumerable<Question> formQuestions)
	{
        this.formQuestions = formQuestions;
        CreateForm();
	}
    private void CreateForm()
    {
        var stackLayout = new StackLayout();
        stackLayout.Children.Add(new Label { Text = "Welcome to the group" });
        foreach (var question in formQuestions)
        {
            View questionControl = GroupEntryForm.CreateQuestionControl(question);
            stackLayout.Children.Add(questionControl);
        }
        var submitButton = SubmitButton();
        stackLayout.Children.Add(submitButton);

        Content = new ScrollView { Content = stackLayout };
    }

    private Button SubmitButton()
    {
        var button = new Button { Text = "Submit" };
        button.Clicked += (sender, e) => HandleSubmit();
        return button;
    }

    private void HandleSubmit()
    {
        Dictionary<string, string> responses = [];
        var questionsLayout = (StackLayout)((ScrollView)Content).Content;
        foreach (var child in questionsLayout.Children)
        {
            if (child is Label || child is Button)
                continue;

            string questionText = ((Label)((StackLayout)child).Children[0]).Text;

            string response = "";
            if (((StackLayout)child).Children[1] is ScrollView scrollView)
            {
                response = "\"" + ((Editor)scrollView.Children[0]).Text + "\"";
            }
            else if (((StackLayout)child).Children[1] is Grid grid)
            {
                foreach (var view in grid.Children)
                {
                    if (view is Slider slider)
                    {
                        response = slider.Value.ToString();
                        break;
                    }
                }
            }
            responses.Add(questionText, response);
        }
        string responseString = string.Join(",", responses.Select(entry => $"{{\n\t{entry.Key}: {entry.Value}\n}}"));
        DisplayAlert("Form Responses", "[\n" + responseString +"\n]", "OK");
    }

    private static View CreateQuestionControl(Question question)
    {
        var QuestionControl = new StackLayout();
        var QuestionText = new Label { Text = question.Text }; 
        QuestionControl.Children.Add(QuestionText);
        switch (question)
        {
            case TextQuestion:
                var scrollView = new ScrollView
                {
                    Content = new Editor()
                };

                // Add the ScrollView containing the Editor to the QuestionControl
                QuestionControl.Children.Add(scrollView);
                break;
            case SliderQuestion sliderQuestion:
                var slider = new Slider
                {
                    Minimum = sliderQuestion.Min,
                    Maximum = sliderQuestion.Max
                };
                var minValueLabel = new Label { Text = sliderQuestion.Min.ToString(), HorizontalOptions = LayoutOptions.Start };
                var maxValueLabel = new Label { Text = sliderQuestion.Max.ToString(), HorizontalOptions = LayoutOptions.End };

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

                QuestionControl.Children.Add(grid);
                break;
            default:
                return new Label { Text = "Unsupported question type" };
        }
        return QuestionControl;
    } 
}