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
        public string Username { get; set; }
        public int PostScore { get; set; }
        public int MarketplaceScore { get; set; }
        public UserStatus Status { get; set; }
        public User(string username)
        {
            Id = Guid.NewGuid();
            Username = username;
            PostScore = 1;
            MarketplaceScore = 1;
            Status =  new(UserRestriction.None, DateTime.Now);
        }
        public User(Guid userId, string username, int postScore, int marketplaceScore, UserStatus userStatus)
        {
            this.Id = userId;
            Username = username;
            PostScore = postScore;
            MarketplaceScore = marketplaceScore;
            this.Status = userStatus;
        }

    }
}
