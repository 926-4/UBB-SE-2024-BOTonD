using Moderation.Entities;

namespace Moderation.JoinRequestView;

public partial class JoinRequestDisplay : ContentView
{ 
	private JoinRequest joinRequest;
	public JoinRequestDisplay(JoinRequest joinRequest)
	{
		this.joinRequest = joinRequest;
		//InitializeComponent();
		CreateView();
	}
	public void CreateView()
	{
        var stackLayout = new StackLayout { Margin = new Thickness(20) };
        var requestLabel = new Label { Text = "Request", FontSize = 20, FontAttributes = FontAttributes.Bold };
        stackLayout.Children.Add(requestLabel);

        var requestIdStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var requestIdLabel = new Label { Text = "Request ID:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        var requestIdValueLabel = new Label { Text = joinRequest.Id.ToString(), FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };
        requestIdStackLayout.Children.Add(requestIdLabel);
        requestIdStackLayout.Children.Add(requestIdValueLabel);
        stackLayout.Children.Add(requestIdStackLayout);

        var userIdStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var userIdLabel = new Label { Text = "User ID:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        var userIdValueLabel = new Label { Text = joinRequest.userId.ToString(), FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };

        userIdStackLayout.Children.Add(userIdLabel);
        userIdStackLayout.Children.Add(userIdValueLabel);
        stackLayout.Children.Add(userIdStackLayout);

        foreach( var pair in joinRequest.messageResponse)
        {
            var grid = new Grid { Margin = new Thickness(0, 10, 0, 0) };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            var keyLabel = new Label { Text = pair.Key, FontSize = 16 };
            var valueLabel = new Label { Text = pair.Value, FontSize = 16 };

            Grid.SetColumn(keyLabel, 0);
            Grid.SetColumn(valueLabel, 1);

            grid.Children.Add(keyLabel);
            grid.Children.Add(valueLabel);

            stackLayout.Children.Add(grid);
        }


        var buttonsStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
        var warningButton = new Button { Text = "Accept" };
        warningButton.Clicked += OnAcceptClicked;
        var muteButton = new Button { Text = "Decline" };
        muteButton.Clicked += OnDeclineClicked;

        Content = stackLayout;
    }

    private void OnDeclineClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void OnAcceptClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}