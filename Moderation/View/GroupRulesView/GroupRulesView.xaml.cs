using Moderation.Model;

namespace Moderation.GroupRulesView;

public partial class GroupRulesView : ContentPage
{
    private readonly IEnumerable<Rule> rules;
    public GroupRulesView(IEnumerable<Rule> r)
    {
        this.rules = r;
        CreateForm();
    }
    private void CreateForm()
    {
        var stackLayout = new StackLayout();

        foreach (var rule in rules)
        {
            var rd = new RuleDisplay(rule);
            stackLayout.Children.Add(rd);
        }
        var backButton = BackButton();
        stackLayout.Children.Add(backButton);

        Content = new ScrollView { Content = stackLayout };
    }


    private Button BackButton()
    {
        var button = new Button { Text = "Back", Margin = 4 };
        button.Clicked += (sender, e) => { Navigation.PopAsync(); };
        return button;
    }


}