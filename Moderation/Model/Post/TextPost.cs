using Moderation.Serivce;

namespace Moderation.Entities.Post
{
    public class TextPost : IPost
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public GroupUser Author { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public List<Award> Awards { get; set; }
        public bool IsDeleted { get; set; }
        //public Guid GroupId { get; set; }
        public TextPost(string content, GroupUser author)
        {
            Id = Guid.NewGuid();
            Content = content;
            Author = author;
            Score = 0;
            Status = string.Empty;
            Awards = [];
            IsDeleted = false;
        }
        public TextPost(string content, GroupUser author, List<Award> awards, int score = 0, string status = "", bool isDeleted = false)
        {
            Id = Guid.NewGuid();
            Content = content;
            Author = author;
            Score = score;
            Status = status;
            Awards = awards;
            IsDeleted = isDeleted;
        }
        public TextPost(Guid id, string content, GroupUser author, List<Award> awards, int score = 0, string status = "", bool isDeleted = false)
        {
            Id = id;
            Content = content;
            Author = author;
            Score = score;
            Status = status;
            Awards = awards;
            IsDeleted = isDeleted;
        }
        public TextPost(Guid id, string content, Guid authorID)
        {
            Id = id;
            Content = content;
            Author = ApplicationState.Get().GroupUsers.Get(authorID)?? throw new Exception("No author by that id");
            Score = 0;
            Status = "";
            Awards = [];
            IsDeleted = false;
        }
    }
}