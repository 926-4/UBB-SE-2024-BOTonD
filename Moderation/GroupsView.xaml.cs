
using Moderation.CurrentSessionNamespace;
using Moderation.Model;

namespace Moderation;

public partial class GroupsView : ContentPage
{
	public GroupsView()
	{
		//InitializeComponent();
        MakeKids();
	}

    private void MakeKids()
    {
        foreach (Group item in ApplicationState.GetApp().Groups.GetAll())
        {
            ((StackLayout)Content).Children.Add(new SingleGroupView(item, CurrentSession.GetInstance().User));
        }
        throw new NotImplementedException();
    }
}