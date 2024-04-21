namespace Moderation.Entities
{
    public class JoinRequest : IHasID
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public JoinRequest(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
        }
        public JoinRequest(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}
