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

        var titleLabel = new Label
        {
            Text = "Welcome to the group",
        };

        stackLayout.Children.Add(titleLabel);
        foreach (var question in formQuestions)
        {
            var questionControl = QuestionDisplayFactory.GetQuestionDisplay(question);
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
            if (child is not QuestionDisplay questionDisplay)
                continue;

            string questionText = questionDisplay.GetQuestion();
            string response = questionDisplay.GetResponse();

            responses.Add(questionText, response);
        }

        string responseString = string.Join(",", responses.Select(entry => $"{{\n\t{entry.Key}: {entry.Value}\n}}"));
        DisplayAlert("Form Responses", "[\n" + responseString +"\n]", "OK");
    }

}