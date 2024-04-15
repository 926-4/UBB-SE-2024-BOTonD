using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities.Post
{
    public interface IPost
    {
        Guid postId { get; set; }
        string content { get; set; }
        User author { get; set; }
        int score { get; set; }
        string status { get; set; }
        List<Award> awards { get; set; }
        bool isDeleted { get; set; }
    }
}
