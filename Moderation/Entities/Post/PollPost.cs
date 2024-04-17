namespace Moderation.Entities.Post
{
    public class PollPost : IPost
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public List<Award> Awards { get; set; }
        public List<String> Options { get; set; }
        public Dictionary<Guid, Guid> Votes { get; set; }

        public PollPost(Guid postId, string content, User author, int score, string status, bool isDeleted, List<string> options, List<Award> awards)
        {
            Id = postId;
            Content = content;
            Author = author;
            Score = score;
            Status = status;
            IsDeleted = isDeleted;
            Awards = awards;
            Options = options;
            Votes = [];
        }

        public void AddOption(string option)
        {
            Options.Add(option);
        }
    }
}
