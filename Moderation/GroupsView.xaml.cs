
using Moderation.CurrentSessionNamespace;
using Moderation.Model;

namespace Moderation;

public partial class GroupsView : ContentPage
{
    public GroupsView()
    {
        Content = new StackLayout { HorizontalOptions = LayoutOptions.Fill };
        MakeKids();
    }

    private void MakeKids()
    {
        foreach (Group item in ApplicationState.Get().Groups.GetAll())
        {
            ((StackLayout)Content).Children.Add(new SingleGroupView(item, CurrentSession.GetInstance().User));
        }
        Button backButton = new()
        {
            Text = "Back",
            HorizontalOptions = LayoutOptions.Fill,
        };
        backButton.Clicked += (s, e) => { CurrentSession.GetInstance().LogOut(); Navigation.PopAsync(); };
        ((StackLayout)Content).Children.Add(backButton);
    }
}