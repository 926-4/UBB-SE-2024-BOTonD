using Moderation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class User(Guid uId, string username, int postScore, int marketplaceScore, UserStatus status) : IHasID
    {
        public Guid Id { get; set; } = uId;
        public string Username { get; set; } = username;
        public int PostScore { get; set; } = postScore;
        public int MarketplaceScore { get; set; } = marketplaceScore;
        public UserStatus Status { get; set; } = status;
    }
}
