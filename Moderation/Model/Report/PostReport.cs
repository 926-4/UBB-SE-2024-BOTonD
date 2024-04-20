namespace Moderation.Entities.Report
{
    public class PostReport : IHasID
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public string Message { get; set; }
        public Guid GroupId { get; set; }
        public PostReport(Guid userId, string message, Guid postId, Guid groupId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            PostId = postId;
            Message = message;
            GroupId = groupId;
        }
        public PostReport(Guid id, Guid userId, string message, Guid postId, Guid groupId)
        {
            Id = id;
            UserId = userId;
            PostId = postId;
            Message = message;
            GroupId = groupId;
        }
    }
}