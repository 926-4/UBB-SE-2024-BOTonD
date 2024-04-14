using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class User
    {
        public Guid uId { get; set; }
        public string username { get; set; }
        public int postScore { get; set; }
        public int marketplaceScore { get; set; }
        public Role role { get; set; }
        public UserStatus status { get; set; }

        public User(Guid uId, string username, int postScore, int marketplaceScore, Role role, UserStatus status)
        {
            this.uId = uId;
            this.username = username;
            this.postScore = postScore;
            this.marketplaceScore = marketplaceScore;
            this.role = role;
            this.status = status;
        }
    }
}
