using Moderation.GroupRulesList;

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
        var groupRulesButton = GroupRulesButton();
        
        var backButton = BackButton();
        stackLayout.Children.Add(groupRulesButton);
        stackLayout.Children.Add(submitButton);
        stackLayout.Children.Add(backButton);

        Content = new ScrollView { Content = stackLayout };
    }
    private void OnGroupRulesClicked()
    {
        Navigation.PushAsync(new GroupRulesList.GroupRulesList([
                new Rule("rule a"),
                    new Rule("rule b"),
                    new Rule("rule c"),
                ]));
    }
    private Button GroupRulesButton()
    {
        var button = new Button { Text = "Group Rules", Margin = 4 };
        button.Clicked += (sender, e) => OnGroupRulesClicked();
        return button;
    }
    private Button SubmitButton()
    {
        var button = new Button { Text = "Submit", Margin = 4 };
        button.Clicked += (sender, e) => HandleSubmit();
        return button;
    }
    private Button BackButton()
    {
        var button = new Button { Text = "Back", Margin = 4 };
        button.Clicked += (sender, e) => { Navigation.PopAsync(); };
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