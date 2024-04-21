using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.GroupEntryForm;
using Moderation.GroupFeed;
using Moderation.Model;
using Moderation.Repository;
using Moderation.Serivce;

namespace Moderation.View;

public class SingleGroupView : ContentView
{
    public SingleGroupView(Group group, User? user)
    {
        if (user == null)
            return;
        var userIsInGroup = group.Creator.Id == user.Id;
        var label = new Label
        {
            Margin = 5,
            Padding = 5,
            VerticalTextAlignment = TextAlignment.Start,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = group.Name
        };
        var viewOrJoinButton = new Button
        {
            Margin = 5,
            Padding = 5,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = userIsInGroup ? "View" : "Join",
        };
        viewOrJoinButton.Clicked += (s, e) =>
        {
            if (userIsInGroup)
            {
                CurrentSession.GetInstance().LookIntoGroup(group);
                TextPostRepository repo = ApplicationState.Get().TextPosts;
                List<TextPost> posts = repo.GetAll().Where(post => post.Author.GroupId == group.Id).ToList();
                GroupFeedView nextPage = new(posts);
                Navigation.PushAsync(nextPage);
            }
            else
            {
                Navigation.PushAsync(new GroupEntryForm.GroupEntryForm(
                    [
                    new TextQuestion("What would you like to be when yoyu grow up?"),
                    new SliderQuestion("How much do you want it?", 0, 100),
                    new RadioQuestion("Pick your favourite farmyard animal:", ["duck", "cow", "pig"])
                    ]));
            }
        };
        var reportButton = new Button
        {
            Margin = 5,
            Padding = 5,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Text = "reports",
        };
        reportButton.Clicked += (s, e) =>
        {
            CurrentSession.GetInstance().LookIntoGroup(group);
            Navigation.PushAsync(new ReportListView.ReportListView(ApplicationState.Get().Reports.GetAll().Where(report => report.GroupId == group.Id)));
        };
        var joinRequestButton = new Button
        {
            Margin = 5,
            Padding = 5,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Text = "requests",
        };
        joinRequestButton.Clicked += (s, e) =>
        {
            CurrentSession.GetInstance().LookIntoGroup(group);
            Navigation.PushAsync(new JoinRequestView.JoinRequestListView(ApplicationState.Get().JoinRequests.GetAll().Where(request => ApplicationState.Get().GroupUsers.Get(request.UserId)?.GroupId == group.Id)));
        };
        if (userIsInGroup)
        {
            Content = new HorizontalStackLayout
            {
                Margin = 5,
                Padding = 5,
                HorizontalOptions = LayoutOptions.Fill,
                Children = {
                label,
                viewOrJoinButton,
                reportButton,
                joinRequestButton
            }
            };
        }
        else
        {
            Content = new HorizontalStackLayout
            {
                Margin = 5,
                Padding = 5,
                HorizontalOptions = LayoutOptions.Fill,
                Children = {
                label,
                viewOrJoinButton,
            }
            };
        }

    }
}