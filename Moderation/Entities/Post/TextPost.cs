using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities.Post
{
    public class TextPost : IPost
    {
        public Guid postId { get; set; }
        public string content { get; set; }
        public User author { get; set; }
        public int score { get; set; }
        public string status { get; set; }
        public List<Award> awards { get; set; }
        public bool isDeleted { get; set; }

        public TextPost(Guid postId, string content, User author, int score, string status, List<Award> awards, bool isDeleted)
        {
            this.postId = postId;
            this.content = content;
            this.author = author;
            this.score = score;
            this.status = status;
            this.awards = awards;
            this.isDeleted = isDeleted;
        }
    }
}
