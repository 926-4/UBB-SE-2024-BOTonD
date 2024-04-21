using Moderation.Entities;

namespace Moderation.Model
{
    public interface IPost : IHasID
    {
        string Content { get; set; }
        GroupUser Author { get; set; }
        int Score { get; set; }
        string Status { get; set; }
        List<Award> Awards { get; set; }
        bool IsDeleted { get; set; }
    }
}
