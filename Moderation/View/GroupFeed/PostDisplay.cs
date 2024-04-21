using Microsoft.Maui.Controls.Shapes;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.GroupFeed;
using Moderation.Model;
using Moderation.Serivce;

namespace Moderation.View.GroupFeed;

public class PostDisplay : ContentView
{
    private readonly IPost post;
    private readonly Picker reactionsPicker;
    private readonly Picker awardsPicker;

    public PostDisplay(IPost post)
    {
        this.post = post;

        Button? reactButton;
        Button? commentButton;
        Button? shareButton;
        Button? awardButton;

        List<string> options = ["Like", "Dislike"];
        reactionsPicker = new Picker
        {
            Title = "React",
            MinimumWidthRequest = 100,
            ItemsSource = options
        };
        reactionsPicker.SelectedIndexChanged += OnReactionsPicker_SelectedIndexChanged;

        List<string> awards = ["Bronze", "Silver", "Gold"];

        awardsPicker = new Picker
        {
            Title = "Award",
            ItemsSource = awards
        };
        awardsPicker.SelectedIndexChanged += OnAwardsPicker_SelectedIndexChanged;

        FlexLayout buttonsLayout = new()
        {
            JustifyContent = Microsoft.Maui.Layouts.FlexJustify.SpaceBetween,
        };

        if (CurrentUserHasPermission(Permission.React))
        {
            reactButton = new Button
            {
                Text = "React"
            };
            reactButton.Clicked += (s, e) =>
            {
                reactButton.Text = (reactButton.Text == "React") ? "Remove reaction" : "React";
            };
            buttonsLayout.Children.Add(reactButton);
        }

        if (CurrentUserHasPermission(Permission.PostComment))
        {
            commentButton = new Button
            {
                Text = "Comment"
            };

            commentButton.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new CommentsFeedView(this.post.Id));
            };
            buttonsLayout.Children.Add(commentButton);
        }

        shareButton = new Button
        {
            Text = "Share"
        };

        shareButton.Clicked += (s, e) =>
        {
            shareButton.Text = (shareButton.Text == "Shared") ? "Remove share" : "Shared";
        };

        buttonsLayout.Children.Add(shareButton);

        awardButton = new Button
        {
            Text = "Award",
        };

        awardButton.Clicked += (s, e) =>
        {
            awardButton.Text = (awardButton.Text == "Awarded") ? "Remove award" : "Awarded";
        };

        buttonsLayout.Children.Add(awardButton);

        StackLayout mainLayout = new()
        {
            Padding = 25,
            Margin = 25,

            Children =
            {
                new Border
                {
                    HorizontalOptions = LayoutOptions.Start,
                    MinimumWidthRequest = 250,

                    Padding = 25,
                    Margin = new Thickness(0, 0, 0, 50),

                    Stroke = Color.FromArgb("#0060ff"),
                    StrokeThickness = 5,
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(15, 15, 15, 15)
                    },
                    Content = new Label
                    {
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Start,
                            Text = ApplicationState.Get().UserRepository.Get(post.Author.UserId)?.Username ?? "",
                            TextColor = Colors.White,

                            FontSize = 25,
                            FontAttributes = FontAttributes.Bold
                    }
                },
                new Border
                {
                    HorizontalOptions = LayoutOptions.Fill,

                    Padding = 25,
                    Margin = new Thickness(0, 0, 0, 50),

					// Border Options
					Stroke = Color.FromArgb("#0060ff"),
                    StrokeThickness = 5,
                    StrokeShape = new RoundRectangle
                    {
                        CornerRadius = new CornerRadius(15, 15, 15, 15)
                    },
                    Content = new Label
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Start,
                        VerticalTextAlignment = TextAlignment.Start,
                        HorizontalTextAlignment = TextAlignment.Start,
                        Text = post.Content,
                        TextColor = Colors.White,

                        FontSize = 25,
                        FontAttributes = FontAttributes.Bold
                    }
                },
                buttonsLayout
            }
        };

        Border border = new()
        {
            BackgroundColor = Color.FromArgb("#0060ff"),
            Margin = new Thickness(50, 50, 50, 50),

            // Border Options
            Stroke = Color.FromArgb("#0060ff"),
            StrokeThickness = 5,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(15, 15, 15, 15)
            },
            Content = mainLayout
        };
        Content = new VerticalStackLayout();
        ((VerticalStackLayout)Content).Children.Add(border);

    }

    private static bool CurrentUserHasPermission(Permission permission)
    {
        User? currentUserMaybe = ApplicationState.Get().CurrentSession.User;
        if (currentUserMaybe == null)
        {
            return false;
        }
        User currentUser = currentUserMaybe;
        Group? currentGroupMaybe = ApplicationState.Get().CurrentSession.Group;
        if (currentGroupMaybe == null)
        {
            return false;
        }
        Group currentGroup = currentGroupMaybe;
        GroupUser? currentGroupUserMaybe = ApplicationState.Get().GroupUsers.GetAll().Where(gu => gu.GroupId == currentGroup.Id && gu.UserId == currentUser.Id).First();
        if (currentGroupUserMaybe == null)
        {
            return false;
        }
        GroupUser currentGroupUser = currentGroupUserMaybe;
        Role? roleMaybe = ApplicationState.Get().Roles.GetAll().Where(r => r.Id == currentGroupUser.RoleId).First();
        if (roleMaybe == null)
        {
            return false;
        }
        Role role = roleMaybe;
        return role.Permissions.Contains(permission);
    }


    private void OnReactionsPicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var selectedReaction = reactionsPicker.SelectedItem.ToString();

        switch (selectedReaction)
        {
            case "Like":
                // TODO

                break;

            case "Dislike":
                // TODO

                break;

            default:
                break;
        }
    }

    private void OnAwardsPicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var selectedAward = awardsPicker.SelectedItem.ToString();

        switch (selectedAward)
        {
            case "Bronze":
                // TODO

                break;

            case "Silver":
                // TODO

                break;

            case "Gold":
                // TODO
                break;

            default:
                break;
        }
    }
}