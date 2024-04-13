namespace Moderation.GroupRulesView;

public partial class RuleDisplay : ContentView
{
	protected Rule rule;
    public RuleDisplay(Rule r)
    {
        this.rule = r;
        InitializeComponent();
        l.Text = GetRule();
    }
    public string GetRule()
    {
        return rule.Text;
    }
}