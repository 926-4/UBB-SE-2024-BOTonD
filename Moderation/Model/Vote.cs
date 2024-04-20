using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class Vote
    {
        public Guid voteId { get; set; }
        public Guid userId { get; set; }
        public Guid pollId { get; set; }
        public string option { get; set; }

        public Vote(Guid voteId, Guid userId, Guid pollId, string option)
        {
            this.voteId = voteId;
            this.userId = userId;
            this.pollId = pollId;
            this.option = option;
        }
    }
}
