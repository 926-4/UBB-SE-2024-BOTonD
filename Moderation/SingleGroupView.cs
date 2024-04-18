using Moderation.CurrentSessionNamespace;
using Moderation.DbEndpoints;
using Moderation.Entities;
using Moderation.GroupFeed;
using Moderation.Model;

namespace Moderation;

public class SingleGroupView : ContentView
{
    public SingleGroupView(Group group, User? user)
    {
        if (user == null)
            return;
        var userIsInGroup = group.Creator.Id == user.Id;// todo change
        var label = new Label
        {
            Margin = 5,
            Padding = 5,
            VerticalTextAlignment = TextAlignment.Start,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Text = group.Name
        };
        var button = new Button
        {
            Margin = 5,
            Padding = 5,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center,
            Text = userIsInGroup ? "View" : "Join",
        };
        button.Clicked += (s, e) =>
        {
            if (userIsInGroup)
            {
                CurrentSession.GetInstance().LookIntoGroup(group);
                Navigation.PushAsync(new GroupFeedView(TextPostEndpoints.ReadAllTextPosts().Where(post => post.Author.GroupId == group.Id)));
            }
            else
            {
                Navigation.PushAsync(new GroupEntryForm.GroupEntryForm(group.GroupEntryQuestions.GetAll()));
            }
        };
        Content = new HorizontalStackLayout
        {
            Margin = 5,
            Padding = 5,
            HorizontalOptions = LayoutOptions.Fill,
            Children = {
                label,
                button
            }
        };
    }
}