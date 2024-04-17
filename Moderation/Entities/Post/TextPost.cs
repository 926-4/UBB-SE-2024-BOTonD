using Moderation.Model;

namespace Moderation.Entities.Post
{
    public class TextPost : IPost
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public User Author { get; set; }
        public int Score { get; set; }
        public string Status { get; set; }
        public List<Award> Awards { get; set; }
        public bool IsDeleted { get; set; }
        public TextPost(string content, User author, string status = "", bool isDeleted = false)
        {
            Id = Guid.NewGuid();
            Content = content;
            Author = author;
            Status = status;
            Score = author.PostScore;
            IsDeleted = isDeleted;
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
