using Moderation.Entities;
using Moderation.Entities.Post;

namespace Moderation.GroupFeed;

public partial class CommentsFeedView : ContentPage
{
	private Guid _postID;

	public CommentsFeedView(Guid postID)
	{
		InitializeComponent();

		_postID = postID;

		CreateFeed();
	}

	private void CreateFeed()
	{
		Layout layout = new StackLayout();

        // TODO remove the hardcoded values
        string content1 = "Nice one.";
        User author1 = new User(System.Guid.NewGuid(), "victor3136", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "enthusiastic"));

        string content2 = "Unbelievable.";
        User author2 = new User(System.Guid.NewGuid(), "SzilagyiBotond", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "at work"));

        string content3 = "Wow.";
        User author3 = new User(System.Guid.NewGuid(), "Cip", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "at peace"));

        string content4 = "...";
        User author4 = new User(System.Guid.NewGuid(), "neon1024_", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "in progress"));

        string content5 = ".";
        User author5 = new User(System.Guid.NewGuid(), "PopNorbert", 0, 0, new UserStatus(UserRestriction.None, System.DateTime.Now, "at the gym"));

        IEnumerable<IPost> _comments = new List<TextPost>
        {
            new TextPost(System.Guid.NewGuid(), content1, author1, 0, "1", new List<Award>{}, false),
            new TextPost(System.Guid.NewGuid(), content2, author2, 0, "2", new List<Award>{}, false),
            new TextPost(System.Guid.NewGuid(), content3, author3, 0, "3", new List<Award>{}, false),
            new TextPost(System.Guid.NewGuid(), content4, author4, 0, "4", new List<Award>{}, false),
            new TextPost(System.Guid.NewGuid(), content5, author5, 0, "5", new List<Award>{}, false)
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