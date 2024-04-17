using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities.Post
{
    public class TextPost(Guid postId, string content, User author, int score, string status, List<Award> awards, bool isDeleted=false) : IPost
    {
        public Guid Id { get; set; } = postId;
        public string Content { get; set; } = content;
        public User Author { get; set; } = author;
        public int Score { get; set; } = score;
        public string Status { get; set; } = status;
        public List<Award> Awards { get; set; } = awards;
        public bool IsDeleted { get; set; } = isDeleted;
    }
}
