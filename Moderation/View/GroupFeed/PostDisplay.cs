using Microsoft.Maui.Controls.Shapes;
using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.GroupFeed;
using Moderation.Model;
using Moderation.Serivce;

namespace Moderation.View.GroupFeed;

public class PostDisplay : ContentView
{
    private readonly IPost _post;
    private readonly Picker _reactionsPicker;
    private readonly Picker _awardsPicker;

    public PostDisplay(IPost post)
    {
        // TODO get the User from the CurrentSession and check if it has permissions
        // TODO modify the userHasPostCommentPermission
        _post = post;

        Button reactButton, commentButton, shareButton, awardButton;

        var reactions = new List<string> { "Like", "Dislike" };

        _reactionsPicker = new Picker
        {
            Title = "React",
            MinimumWidthRequest = 100,
            ItemsSource = reactions
        };
        _reactionsPicker.SelectedIndexChanged += OnReactionsPicker_SelectedIndexChanged;

        var awards = new List<string> { "Bronze", "Silver", "Gold" };

        _awardsPicker = new Picker
        {
            Title = "Award",
            ItemsSource = awards
        };
        _awardsPicker.SelectedIndexChanged += OnAwardsPicker_SelectedIndexChanged;

        FlexLayout buttonsLayout = new()
        {
            JustifyContent = Microsoft.Maui.Layouts.FlexJustify.SpaceBetween,

        };

        if (PostDisplay.UserHasReactPermission(post))
        {
            reactButton = new Button
            {
                Text = "React"
            };

            reactButton.Command = new Command(() =>
            {
                reactButton.Text = "Reacted";
            });

            if (reactButton != null)
            {
                buttonsLayout.Children.Add(reactButton);
            }

            buttonsLayout.Children.Add(reactButton);
        }

        if (PostDisplay.UserHasPostCommentPermission(post))
        {
            commentButton = new Button
            {
                Text = "Comment",
                Command = new Command(() =>
                {
                    Navigation.PushAsync(new CommentsFeedView(_post.Id));
                })
            };

            buttonsLayout.Children.Add(commentButton);
        }

        shareButton = new Button
        {
            Text = "Share"
        };

        shareButton.Command = new Command(() =>
        {
            shareButton.Text = "Shared";
        });

        if (shareButton != null)
        {
            buttonsLayout.Children.Add(shareButton);
        }

        awardButton = new Button
        {
            Text = "Award",
        };

        awardButton.Command = new Command(() =>
        {
            awardButton.Text = "Awarded";
        });

        if (awardButton != null)
        {
            buttonsLayout.Children.Add(awardButton);
        }

        StackLayout mainLayout = new()
        {
            Padding = 25,
            Margin = new Thickness(25, 25, 25, 25),

            Children =
            {
                new Border
                {
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
                        },

                        HorizontalOptions = LayoutOptions.Start,
                        MinimumWidthRequest = 250,

                        Padding = 25,
                        Margin = new Thickness(0, 0, 0, 50),

						// Border Options
						Stroke = Color.FromArgb("#1e2124"),
                        StrokeThickness = 5,
                        StrokeShape = new RoundRectangle
                        {
                            CornerRadius = new CornerRadius(15, 15, 15, 15)
                        }
                    },

                new Border {

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
                        },

                        HorizontalOptions = LayoutOptions.Fill,

                        Padding = 25,
                        Margin = new Thickness(0, 0, 0, 50),

						// Border Options
						Stroke = Color.FromArgb("#1e2124"),
                        StrokeThickness = 5,
                        StrokeShape = new RoundRectangle
                        {
                            CornerRadius = new CornerRadius(15, 15, 15, 15)
                        }
                    },

                buttonsLayout
            }
        };

        Border border = new()
        {
            Content = mainLayout,

            BackgroundColor = Color.FromArgb("#36393e"),
            Margin = new Thickness(50, 50, 50, 50),

            // Border Options
            Stroke = Color.FromArgb("#1e2124"),
            StrokeThickness = 5,
            StrokeShape = new RoundRectangle
            {
                CornerRadius = new CornerRadius(15, 15, 15, 15)
            }
        };

        Content = border;
    }

    private static bool UserHasReactPermission(IPost post)
    {
        if (post == null)
            return false;

        Guid currentUserID = CurrentSession.GetInstance().User?.Id
            ?? throw new Exception("Current user is null"); ;
        GroupUser currentUser = ApplicationState.Get().GroupUsers.GetByUserIdAndGroupId(currentUserID, post.Author.GroupId)
                        ?? throw new Exception("Current user is null");

        Role role = ApplicationState.Get().Roles.Get(currentUser.RoleId)
            ?? throw new Exception("Role of current user is null");

        if (role == null)
            return false;

        return role.Permissions.Contains(Permission.React);
    }

    private static bool UserHasPostCommentPermission(IPost post)
    {
        if (post == null)
            return false;
        Guid currentUserID = CurrentSession.GetInstance().User?.Id
            ?? throw new Exception("Current user is null"); ;
        GroupUser currentUser = ApplicationState.Get().GroupUsers.GetByUserIdAndGroupId(currentUserID, post.Author.GroupId)
                        ?? throw new Exception("Current user is null");


        Role role = ApplicationState.Get().Roles.Get(currentUser.RoleId)
            ?? throw new Exception("Role of current user is null");

        if (role == null)
            return false;

        return role.Permissions.Contains(Permission.PostComment);
    }

    private void OnReactionsPicker_SelectedIndexChanged(object? sender, EventArgs e)
    {
        var selectedReaction = _reactionsPicker.SelectedItem.ToString();

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
        var selectedAward = _awardsPicker.SelectedItem.ToString();

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