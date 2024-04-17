using Moderation.CurrentSessionNamespace;
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
		var userIsInGroup = group.Roles.Contains(user.Id);
		var label = new Label
		{
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			Text = group.Name
		};
		var button = new Button
		{
			HorizontalOptions = LayoutOptions.Center,
			VerticalOptions = LayoutOptions.Center,
			Text = userIsInGroup ? "View" : "Join",
		};
		button.Clicked += (s, e) =>
		{
			if(userIsInGroup)
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
			Children = {
				label,
				button
			}
		};
	}
}