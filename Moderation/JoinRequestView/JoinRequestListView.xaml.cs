using Moderation.Entities;

namespace Moderation.JoinRequestView;

public partial class JoinRequestListView : ContentPage
{
	private IEnumerable<JoinRequest> joinRequests;
	public JoinRequestListView(IEnumerable<JoinRequest> joinRequests)
	{
		this.joinRequests = joinRequests;
		//InitializeComponeknt();
		CreateList();
	}
	private void CreateList()
	{
        var stackLayout = new StackLayout();

        foreach (var request in joinRequests)
        {
            var requestControl = new JoinRequestDisplay(request);
            stackLayout.Children.Add(requestControl);
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