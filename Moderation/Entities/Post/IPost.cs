using Moderation.Model;

namespace Moderation.Entities.Post
{
    public interface IPost : IHasID
    {
        string Content { get; set; }
        User Author { get; set; }
        int Score { get; set; }
        string Status { get; set; }
        List<Award> Awards { get; set; }
        bool IsDeleted { get; set; }
    }
}
