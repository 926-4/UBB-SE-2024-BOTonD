using Moderation.CurrentSessionNamespace;
using Moderation.Entities;
using Moderation.Entities.Post;
using Moderation.Model;

namespace Moderation.GroupFeed;

public partial class CommentsFeedView : ContentPage
{
	private readonly Guid postId;

	public CommentsFeedView(Guid postID)
	{
		//InitializeComponent();

		postId = postID;

		CreateFeed();
	}

	private void CreateFeed()
	{
		Layout layout = new StackLayout();
        Group group = CurrentSession.GetInstance().Group ?? throw new Exception("Curret group does not exist");
        IEnumerable<IPost> _comments = new List<TextPost>
        {
            new("Nice one.", new GroupUser(Guid.Parse("0478C96C-548B-427C-96A1-006291F50C7B"), group.Id),[]),
            new("Unbelievable.", new GroupUser(Guid.Parse("59B2E634-EA1E-48E1-9C9A-05E4ED8A8011"), group.Id),[]),
            new("Wow.",new GroupUser(Guid.Parse("FBAC81EF-BD49-45F6-8EB5-0DD21FD851C0"), group.Id),[]),
            new("...", new GroupUser(Guid.Parse("0CA3CAF2-161D-435B-9B06-17C63BA1EBA9"), group.Id),[]),
            new(".", new GroupUser(Guid.Parse("05647D11-F9C6-49DE-B5B5-1E072657F963"), group.Id),[])
        };  

        foreach (var comment in _comments)
        {
            layout.Children.Add(new View.GroupFeed.PostDisplay(comment));
        }

        Content = new ScrollView
        {
            Content = layout,
            BackgroundColor = Color.FromArgb("#424549"),
        };
    }
}