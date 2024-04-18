using Moderation.Entities;
using Moderation.Entities.Post;

namespace Moderation.GroupFeed;

public partial class CommentsFeedView : ContentPage
{
	private Guid _postID;

	public CommentsFeedView(Guid postID)
	{
		//InitializeComponent();

		_postID = postID;

		CreateFeed();
	}

	private void CreateFeed()
	{
		Layout layout = new StackLayout();

        IEnumerable<IPost> _comments = new List<TextPost>
        {
            //new("Nice one.", new User("victor3136")),
            //new("Unbelievable.", new User("SzilagyiBotond")),
            //new("Wow.", new User("Cip")),
            //new("...", new User("neon1024_")),
            //new(".", new User("PopNorbert"))
        };  

        foreach (var comment in _comments)
        {
            layout.Children.Add(new PostDisplay(comment));
        }

        Content = new ScrollView
        {
            Content = layout,
            BackgroundColor = Color.FromArgb("#424549"),
        };
    }
}