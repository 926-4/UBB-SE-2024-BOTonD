using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.Entities.Report;
using Moderation.View.GroupFeed;

namespace Moderation.ReportListView;

public partial class ReportDisplay : ContentView
{
    private PostReport postReport;
    public ReportDisplay(PostReport report)
    {
        this.postReport = report;
        var stackLayout = new StackLayout { Margin = new Thickness(20) };

        var reportIdStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var reportIdLabel = new Label { Text = "Report ID:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        var reportIdValueLabel = new Label { Text = report.Id.ToString(), FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };
        reportIdStackLayout.Children.Add(reportIdLabel);
        reportIdStackLayout.Children.Add(reportIdValueLabel);
        stackLayout.Children.Add(reportIdStackLayout);

        var userIdStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var userIdLabel = new Label { Text = "User Name:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        GroupUser groupUser=GroupUserEndpoints.ReadAllGroupUsers().Where(guser => guser.Id==report.UserId && guser.GroupId==report.GroupId).ToArray()[0];
        User user=UserEndpoints.ReadAllUsers().Where(user => user.Id == groupUser.UserId).ToArray()[0];
        var userIdValueLabel = new Label { Text = user.Username, FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };
        
        userIdStackLayout.Children.Add(userIdLabel);
        userIdStackLayout.Children.Add(userIdValueLabel);
        stackLayout.Children.Add(userIdStackLayout);

        //stackLayout.Children.Add(new PostDisplay(TextPostEndpoints.ReadAllTextPosts().Where(post => post.Id == report.PostId).ToArray()[0]));

        var reportedUserNameStackLayout = new StackLayout { Orientation = StackOrientation.Horizontal };
        var reportedUserNameLabel = new Label { Text = "Reported User Name:", FontSize = 16, Margin = new Thickness(0, 4, 10, 0) };
        TextPost post = TextPostEndpoints.ReadAllTextPosts().Where(post => post.Id == report.PostId).ToArray()[0];
        GroupUser reportedGroupUser = GroupUserEndpoints.ReadAllGroupUsers().Where(guser => guser.Id==post.Author.Id).ToArray()[0];
        User reportedUser = UserEndpoints.ReadAllUsers().Where(user => user.Id == reportedGroupUser.UserId).ToArray()[0];
        var reportedUserNameValue = new Label { Text = reportedUser.Username, FontSize = 16, Margin = new Thickness(0, 4, 0, 0) };

        reportedUserNameStackLayout.Children.Add(reportedUserNameLabel);
        reportedUserNameStackLayout.Children.Add(reportedUserNameValue);
        stackLayout.Children.Add(reportedUserNameStackLayout);

        var messageEntry = new Entry { Text = report.Message, Margin = new Thickness(0, 4, 0, 0), IsReadOnly=true };
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

    private void OnWarningClicked(object? sender, EventArgs e)
    {
        // Handle Warning button click
    }

    private void OnMuteClicked(object? sender, EventArgs e)
    {
        // Handle Mute button click
    }

    private void OnBanClicked(object? sender, EventArgs e)
    {
        // Handle Ban button click
    }

    private void OnDismissClicked(object? sender, EventArgs e)
    {
        // Handle Dismiss button click
    }

}