using Moderation.Model;

namespace Moderation.Entities.Report
{
    public class PostReport : IHasID
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Message { get; set; }
        public Guid PostId { get; set; }
        public PostReport(Guid userId, string message, Guid postId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Message = message;
            PostId = postId;
        }
        public PostReport(Guid id, Guid userId, string message, Guid postId)
        {
            Id = id;
            UserId = userId;
            Message = message;
            PostId = postId;
        }
    }
}
