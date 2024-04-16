using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities.Post
{
    public class PollPost : IPost
    {
        public Guid postId { get; set; }
        public string content { get; set; }
        public User author { get; set; }
        public int score { get; set; }
        public string status { get; set; }
        public bool isDeleted { get; set; }
        public List<Award> awards { get; set; }
        public List<String> Options { get; set; }
        public Dictionary<Guid, Guid> Votes { get; set; }

        public PollPost(Guid postId, string content, User author, int score, string status, bool isDeleted, List<string> options, List<Award> awards)
        {
            this.postId = postId;
            this.content = content;
            this.author = author;
            this.score = score;
            this.status = status;
            this.isDeleted = isDeleted;
            this.awards = awards;
            Options = options;
            Votes = [];
            this.awards = awards;
        }

        public void AddOption(string option)
        {
            Options.Add(option);
        }
    }
}
