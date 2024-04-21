using Moderation.Entities;

namespace Moderation.Model
{
    public class PostReport : IHasID
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Message { get; set; }
        public Guid GroupId { get; set; }
        public PostReport(Guid userId, Guid postId, string message, Guid groupId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Message = message;
            PostId = postId;
            GroupId = groupId;
        }
        public PostReport(Guid id, Guid userId, Guid postId, string message, Guid groupId)
        {
            Id = id;
            UserId = userId;
            PostId = postId;
            Message = message;
            GroupId = groupId;
        }
    }
}