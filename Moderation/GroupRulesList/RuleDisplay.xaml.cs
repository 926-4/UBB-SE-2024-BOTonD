namespace Moderation.GroupRulesList;

public partial class RuleDisplay : ContentView
{
	protected Rule rule;
	public RuleDisplay(Rule r)
	{
		rule = r;
		InitializeComponent();
		l.Text = getRule();
	}
	public string getRule()
	{
		return rule.Text;
	}
}