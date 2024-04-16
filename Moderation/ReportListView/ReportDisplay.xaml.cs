using Moderation.Entities.Report;

namespace Moderation.ReportListView;

public partial class ReportDisplay : ContentView
{
	private PostReport postReport;
	public ReportDisplay(PostReport report)
	{
		this.postReport = report;
        var stackLayout = new StackLayout { Margin = new Thickness(20) };

        var reportLabel = new Label { Text = "Report", FontSize = 20, FontAttributes = FontAttributes.Bold };
        stackLayout.Children.Add(reportLabel);

        var reportIdStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var reportIdLabel = new Label { Text = "Report ID:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        var reportIdValueLabel = new Label { Text = report.reportId.ToString(), FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };
        reportIdStackLayout.Children.Add(reportIdLabel);
        reportIdStackLayout.Children.Add(reportIdValueLabel);
        stackLayout.Children.Add(reportIdStackLayout);

        var userIdStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var userIdLabel = new Label { Text = "User ID:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        var userIdValueLabel = new Label { Text = report.userId.ToString(), FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };
        userIdStackLayout.Children.Add(userIdLabel);
        userIdStackLayout.Children.Add(userIdValueLabel);
        stackLayout.Children.Add(userIdStackLayout);

        var messageEntry = new Entry { Text = report.message, Placeholder = "Enter message...", Margin = new Thickness(0, 4, 0, 0) };
        stackLayout.Children.Add(messageEntry);

        var buttonsStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal, Margin = new Thickness(0, 10, 0, 0) };
        var warningButton = new Button { Text = "Warning" };
        warningButton.Clicked += OnWarningClicked;
        var muteButton = new Button { Text = "Mute" };
        muteButton.Clicked += OnMuteClicked;
        var banButton = new Button { Text = "Ban" };
        banButton.Clicked += OnBanClicked;
        var dismissButton = new Button { Text = "Dismiss" };
        dismissButton.Clicked += OnDismissClicked;
        buttonsStackLayout.Children.Add(warningButton);
        buttonsStackLayout.Children.Add(muteButton);
        buttonsStackLayout.Children.Add(banButton);
        buttonsStackLayout.Children.Add(dismissButton);
        stackLayout.Children.Add(buttonsStackLayout);

        Content = stackLayout;
    }
    private void CreateContent()
    {

    }
    private void OnWarningClicked(object sender, EventArgs e)
    {
        // Handle Warning button click
    }

    private void OnMuteClicked(object sender, EventArgs e)
    {
        // Handle Mute button click
    }

    private void OnBanClicked(object sender, EventArgs e)
    {
        // Handle Ban button click
    }

    private void OnDismissClicked(object sender, EventArgs e)
    {
        // Handle Dismiss button click
    }

}