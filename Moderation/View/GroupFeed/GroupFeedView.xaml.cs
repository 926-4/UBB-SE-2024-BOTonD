using Moderation.Entities.Post;
using Moderation.View.GroupFeed;
using ScrollView = Microsoft.Maui.Controls.ScrollView;

namespace Moderation.GroupFeed;

public partial class GroupFeedView : ContentPage
{

    public GroupFeedView(IEnumerable<IPost> posts)
    {
        PostsView = [];
        CreateFeed(posts);
    }

    private void CreateFeed(IEnumerable<IPost> posts)
    {
        foreach (var post in posts)
        {
            PostsView.Children.Add(new PostDisplay(post));
        }
    }
}