using Moderation.Model;

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
        public Guid GroupId { get; set; }
        public TextPost(string content, GroupUser author, Guid groupId, string status = "", bool isDeleted = false)
        {
            Id = Guid.NewGuid();
            Content = content;
            Author = author;
            Status = status;
            Score = author.PostScore;
            IsDeleted = isDeleted;
            GroupId=groupId;
            Awards = [];
        }
        public TextPost(Guid id, string content,int score ,string status, bool isDeleted, Guid groupId)
        {
            Id = id;
            Content = content;
            Score=score;
            Status = status;
            IsDeleted = isDeleted;
            GroupId=groupId;
            Awards = [];
        }
        //public TextPost(Guid first, Guid second, String text, Guid third)
        //{
        //    Id = first;
        //    Author = ApplicationState.Get().UserRepository.Get(second);
        //    Content = text;

        //}
    }
}
