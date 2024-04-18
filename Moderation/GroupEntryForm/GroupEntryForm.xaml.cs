using Moderation.CurrentSessionNamespace;
using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.GroupRulesView;
using Moderation.Model;
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
    private Button GroupRulesButton()
    {
        var button = new Button { Text = "Group Rules", Margin = 4 };
        button.Clicked += (sender, e) => HandleRules();
        return button;
    }
    private void HandleRules()
    {
        List<Rule> rules = [];
        foreach( Rule rule in CurrentSession.GetInstance().Group.GroupRules.GetAll())
        {
            rules.Add(rule);
        }
        Navigation.PushAsync(new GroupRulesView.GroupRulesView(rules));
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
        Role role = new Role(Guid.NewGuid(), "Pending");
        RoleEndpoints.CreateRole(role);
        GroupUser groupUser = new GroupUser(CurrentSession.GetInstance().User.Id,CurrentSession.GetInstance().Group.Id,role.Id);
        CurrentSession.GetInstance().Group.GroupMembers.Add(CurrentSession.GetInstance().User,role);
        //ApplicationState.Get().GroupUserRepository.Add(groupUser.Id, groupUser);
        JoinRequest request = new JoinRequest(groupUser.Id);
        ApplicationState.Get().JoinRequests.Add(request.Id,request);
        string responseString = "{\n" +
                                $"\tuser: {CurrentSession.GetInstance()?.User?.Username},\n" +
                                $"\ttime: {DateTime.Now},\n" +
                                "\t[\n" + string.Join(",", responses.Select(entry => $"{{\n\t{entry.Key}: {entry.Value}\n}}")) + "\n]\n" +
                                "}";
        DisplayAlert("Thanks for applying! We'll get back to you soon",$"Form Responses : {responseString}", "OK");
    }

}