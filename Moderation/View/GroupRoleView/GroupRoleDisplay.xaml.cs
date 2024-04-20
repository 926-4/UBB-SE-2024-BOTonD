using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Model;

namespace Moderation.GroupRoleView;

public partial class GroupRoleDisplay : ContentPage
{
    private Role role;
    private CurrentSession CurrentSession = CurrentSession.GetInstance();

    public GroupRoleDisplay(Role role)
	{
		this.role = role;
		InitializeComponent();
	}

    public void CreateRoleView()
    {
        var stackLayout = new StackLayout { Margin = new Thickness(20) };

        if (CurrentSession != null && CurrentSession.Group != null)
        {
            var currentGroup = CurrentSession.Group;

            if (currentGroup.Roles.Contains(role.Id))
            {
                foreach (var user in currentGroup.GroupMembers)
                {
                    if (user.Value.Id.Equals(role.Id))
                    {
                        var usernameLabel = new Label { Text = user.Key.Username, FontSize = 16, FontAttributes = FontAttributes.Bold };

                        stackLayout.Children.Add(usernameLabel);
                    }
                }
            }
            else
            {
                var errorLabel = new Label { Text = $"The desired role '{role.Name}' does not exist in the current group." };
                stackLayout.Children.Add(errorLabel);
            }
        }
        else
        {
            var errorLabel = new Label { Text = "No active session or group found." };
            stackLayout.Children.Add(errorLabel);
        }
        Content = stackLayout;
    }
}