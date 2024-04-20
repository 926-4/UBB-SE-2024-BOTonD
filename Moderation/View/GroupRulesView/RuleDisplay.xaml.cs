using Moderation.Model;

namespace Moderation.GroupRulesView;

public partial class RuleDisplay : ContentView
{
	protected Rule rule;
    public RuleDisplay(Rule newRule)
    {
        rule = newRule;
        InitializeComponent();
        RuleTextDisplay.Text = GetRuleText();
    }
    public string GetRuleText()
    {
        return rule.Text;
    }
}