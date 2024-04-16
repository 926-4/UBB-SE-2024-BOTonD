using Microsoft.Maui.Controls.Shapes;
using Moderation.Entities.Post;
using Path = System.IO.Path;

namespace Moderation.GroupFeed;

public class PostDisplay : ContentView
{
    public PostDisplay(IPost post)
	{
        Button reactButton, commentButton, shareButton, awardButton;

        reactButton = new Button
        {
            Text = "React"
        };

        reactButton.Command = new Command(() =>
        {
            // Change the button text when clicked
            reactButton.Text = "Reacted";

            // Action to perform when React button is clicked
            // Example: Display react options or perform specific action
        });

        commentButton = new Button
        {
            Text = "Comment"
        };

        commentButton.Command = new Command(() =>
        {
            commentButton.Text = "Commented";
        });

        shareButton = new Button
        {
            Text = "Share"
        };

        shareButton.Command = new Command(() =>
        {
            shareButton.Text = "Shared";
        });

        awardButton = new Button
        {
            Text = "Award",
        };

        awardButton.Command = new Command(() =>
        {
            awardButton.Text = "Awarded";
        });

        // Adding buttons to the layout
        FlexLayout buttonsLayout = new FlexLayout()
        {
            JustifyContent = Microsoft.Maui.Layouts.FlexJustify.SpaceBetween,

            Children =
            {
               reactButton,
               commentButton,
               shareButton,
               awardButton
            }
        };

		StackLayout mainLayout = new StackLayout
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

                            Text = post.author.Username,
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

                            Text = post.content,
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

        Border border = new Border
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
}