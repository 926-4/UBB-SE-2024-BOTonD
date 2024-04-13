namespace Moderation.GroupRulesView;

public partial class RuleDisplay : ContentView
{
	protected Rule rule;
    public RuleDisplay(Rule newRule)
    {
        this.rule = newRule;
        InitializeComponent();
        RuleTextDisplay.Text = GetRuleText();
    }
    public string GetRuleText()
    {
        return rule.Text;
    }
}