using Moderation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class User : IHasID
    {
        public Guid Id { get; set; }
        public string username { get; set; }
        public int postScore { get; set; }
        public int marketplaceScore { get; set; }
        public UserStatus status { get; set; }

        public User(Guid uId, string username, int postScore, int marketplaceScore, UserStatus status)
        {
            this.Id = uId;
            this.username = username;
            this.postScore = postScore;
            this.marketplaceScore = marketplaceScore;
            this.status = status;
        }
    }
}
