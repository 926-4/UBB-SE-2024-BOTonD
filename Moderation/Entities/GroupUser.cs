using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class GroupUser
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid GroupId { get; set; }
        public int PostScore { get; set; }
        public int MarketplaceScore { get; set; }
        public UserStatus Status { get; set; }
        public Guid RoleId { get; set; }
        public GroupUser(Guid userId, Guid groupId)
        {
            Id = Guid.NewGuid();
            UserId=userId;
            GroupId = groupId;
            PostScore = 1;
            MarketplaceScore = 1;
            Status = new(UserRestriction.None, DateTime.Now);
        }
        public GroupUser(Guid id,Guid userId, Guid groupId, int postScore, int marketplaceScore, UserStatus userStatus)
        {
            Id = id;
            UserId = userId;
            GroupId = groupId;
            PostScore = postScore;
            MarketplaceScore = marketplaceScore;
            Status = userStatus;
        }
        public GroupUser(Guid id, Guid userId, Guid groupId, int postScore, int marketplaceScore, UserStatus userStatus, Guid roleId)
        {
            Id = id;
            UserId = userId;
            GroupId = groupId;
            PostScore = postScore;
            MarketplaceScore = marketplaceScore;
            Status = userStatus;
            RoleId = roleId;
        }
    }
}