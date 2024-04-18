namespace Moderation.Entities.Post
{
    public class PollPost(Guid postId,
        string content, 
        GroupUser author, 
        int score, 
        string status, 
        List<string> options, 
        List<Award> awards, 
        Guid groupId, 
        bool isDeleted = false) : IPost
    { 
        public Guid Id { get; set; } = postId;
        public string Content { get; set; } = content;
        public GroupUser Author { get; set; } = author;
        public int Score { get; set; } = score;
        public string Status { get; set; } = status;
        public bool IsDeleted { get; set; } = isDeleted;
        public List<Award> Awards { get; set; } = awards;
        public List<String> Options { get; set; } = options;
        public Dictionary<Guid, Guid> Votes { get; set; } = [];

        public Guid GroupId { get; set; } = groupId;


        public void AddOption(string option)
        {
            Options.Add(option);
        }
    }
}