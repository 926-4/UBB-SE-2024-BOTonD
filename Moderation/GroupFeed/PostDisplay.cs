using Moderation.Entities.Post;

namespace Moderation.GroupFeed;

public class PostDisplay : ContentView
{
	public PostDisplay(IPost post)
	{
		Content = new StackLayout
		{
            Padding = 25,
			Margin = new Thickness(25, 50, 25, 50),
			WidthRequest = 1000,

            Children =
			{
				new Label() {Text=post.author.Username, HorizontalOptions=LayoutOptions.Start, TextColor=Colors.White, FontSize=20, FontAttributes=FontAttributes.Bold},
				new Label() {Text=post.content, HorizontalOptions=LayoutOptions.Start, TextColor=Colors.White, FontSize=20, FontAttributes=FontAttributes.Bold},
				
				// TODO add post buttons
				new FlexLayout()
				{
					JustifyContent=Microsoft.Maui.Layouts.FlexJustify.SpaceBetween,

					Children =
					{
						new Label() {Text="react", HorizontalOptions=LayoutOptions.Start, TextColor=Colors.White, FontSize=20, FontAttributes=FontAttributes.Bold, WidthRequest=100},
						new Label() {Text="comment", HorizontalOptions=LayoutOptions.Start, TextColor=Colors.White, FontSize=20, FontAttributes=FontAttributes.Bold, WidthRequest=100},
						new Label() {Text="share", HorizontalOptions=LayoutOptions.Start, TextColor=Colors.White, FontSize=20, FontAttributes=FontAttributes.Bold, WidthRequest=100},
						new Label() {Text="award", HorizontalOptions=LayoutOptions.Start, TextColor=Colors.White, FontSize=20, FontAttributes=FontAttributes.Bold, WidthRequest=100}
					}
				},
			}
		};

		BackgroundColor = Color.FromArgb("#36393e");
	}
}