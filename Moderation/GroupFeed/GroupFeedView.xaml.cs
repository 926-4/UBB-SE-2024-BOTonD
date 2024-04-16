 using Moderation.Entities.Post;
using ScrollView = Microsoft.Maui.Controls.ScrollView;

namespace Moderation.GroupFeed;

public partial class GroupFeedView : ContentPage
{
	private IEnumerable<IPost> _posts;

	public GroupFeedView(IEnumerable<IPost> posts)
	{
		InitializeComponent();

		_posts = posts;

		CreateFeed();
	}

	private void CreateFeed()
	{
		Layout layout = new StackLayout();

		foreach (var post in _posts)
		{
			layout.Children.Add(new PostDisplay(post));
		}

		Content = new ScrollView {
			Content = layout,
			BackgroundColor = Color.FromArgb("#424549"),
			Margin = new Thickness(0, 100, 0, 100)
		};
	}
}