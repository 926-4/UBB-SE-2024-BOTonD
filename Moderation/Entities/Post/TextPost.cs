using Microsoft.IdentityModel.Tokens;
using Moderation.Model;
using static Android.Provider.UserDictionary;

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
        public TextPost(string content,  GroupUser author)
        {
            Id = Guid.NewGuid();
            Content = content;
            Author = author;
            Score = author.PostScore;
            Status = string.Empty;
            Awards = new List<Award>{};
            IsDeleted = false;
        }
        public TextPost(string content, GroupUser author, List<Award> awards, int score = 0, string status = "", bool isDeleted = false)
        {
            Id = Guid.NewGuid();
            Content = content;
            Author = author;
            Score = author.PostScore + score;
            Status = status;
            Awards = awards;
            IsDeleted = isDeleted;
        }
        public TextPost(Guid id, string content, GroupUser author, List<Award> awards, int score = 0, string status = "", bool isDeleted = false)
        {
            Id = id;
            Content = content;
            Author = author;
            Score = author.PostScore + score;
            Status = status;
            Awards = awards;
            IsDeleted = isDeleted;
        }
    }
}