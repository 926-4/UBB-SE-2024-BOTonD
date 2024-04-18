using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation;

public class SingleGroupView : ContentView
{
    public SingleGroupView(Group group, User? user)
    {
        if (user == null)
            return;
        var userIsInGroup = group.Roles.Contains(user.Id);
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
                //Navigation.PushAsync(new GroupFeedView(ApplicationState.Get().Posts));
                CurrentSession.GetInstance().LookIntoGroup(group);
                Navigation.PopAsync();
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