using System.Runtime.CompilerServices;

namespace Moderation.GroupRulesList;

public partial class GroupRulesList : ContentPage
{
    private readonly IEnumerable<Rule> Rules; 
	public GroupRulesList(IEnumerable<Rule> r)
	{
        Rules = r;
		InitializeComponent();
        CreateList();
	}
    private void CreateList()
    {
        var stackLayout = new StackLayout();

        foreach (var rule in Rules)
        {
            var ruleControl = new RuleDisplay(rule);
            stackLayout.Children.Add(ruleControl);
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