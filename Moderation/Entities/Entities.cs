using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class User
    {
        public int uId { get; set; }
        public string username { get; set; }
        public int postScore { get; set; }
        public int marketplaceScore { get; set; }
        public Role role { get; set; }
        public UserStatus status { get; set; }

        public User(int uId, string username, int postScore, int marketplaceScore, Role role, UserStatus status)
        {
            this.uId = uId;
            this.username = username;
            this.postScore = postScore;
            this.marketplaceScore = marketplaceScore;
            this.role = role;
            this.status = status;
        }

        public bool HasPermission(string permissionName)
        {
            if (role != null && role.Permissions != null)
            {
                return role.Permissions.Any(p => p.Name == permissionName);
            }
            return false;
        }
    }

    public enum UserRestriction
    {
        None,
        Muted,
        Banned
    }

    public class UserStatus
    {
        public UserRestriction restriction { get; set; }
        public DateTime restrictionDate { get; set; }
        public string message { get; set; }

        public UserStatus(UserRestriction restriction, DateTime restrictionDate, string message)
        {
            this.restriction = restriction;
            this.restrictionDate = restrictionDate;
            this.message = message;
        }
    }


    public class Role
    {
        public int roleId { get; set; }
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }

        public Role(int roleId, string name)
        {
            this.roleId = roleId;
            Name = name;
            Permissions = new List<Permission>();
        }

        public void addPermission(Permission permissions)
        {
            Permissions.Add(permissions);
        }

        public void removePermission(string permissionName)
        {
            Permissions.RemoveAll(p => p.Name == permissionName);
        }
    }

    public class Permission
    {
        public int permissionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        private enum PermissionType
        {
            CreatePost,
            EditOwnPost,
            RemoveOwnPost,
            PostComment,
            EditOwnComment,
            RemoveOwnComment,
            React,
            UpdateOwnReaction,
            RemoveOwnReaction,
            ReportPost,
            ReportComment,
            CreateEvent,
            RemovePost,
            RemoveComment,
            CreateRole,
            AddPermisionToRole,
            RemovePermisionFromRole,
            AssignRole,
            RevokeRole,
            ResolveRequest,
            InviteFriends
        }

        public Permission(int permissionId ,string name, string description)
        {
            permissionID = permissionId;
            Name = name;
            Description = description;
        }
    }

    public interface IPost
    {
        int postId { get; set; }
        string content { get; set; }
        User author { get; set; }
        int score { get; set; }
        string status { get; set; }
        List<Award> awards { get; set; }
        bool isDeleted { get; set; }
    }

    public class Post : IPost
    {
        public int postId { get; set; }
        public string content { get; set; }
        public User author { get; set; }
        public int score { get; set; }
        public string status { get; set; }
        public List<Award> awards { get; set; }
        public bool isDeleted { get; set; }

        public Post(int postId, string content, User author, int score, string status, List<Award> awards, bool isDeleted)
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

    public class Comment : IPost
    {
        public int postId { get; set;}
        public string content { get; set; }
        public User author { get; set; }
        public int score { get; set; }
        public string status { get; set; }
        public List<Award> awards { get; set; }
        public bool isDeleted { get; set; }

        public Comment(int postId, string content, User author, int score, string status, List<Award> awards, bool isDeleted)
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

    public class Poll : IPost
    {
        public int postId { get; set;}
        public string content { get; set; }
        public User author { get; set; }
        public int score { get; set; }
        public string status { get; set; }
        public bool isDeleted { get; set; }
        public List<Award> awards { get; set; }
        public List<String> options { get; set; }
        public Dictionary<int, int> votes { get; set; }

        public Poll(int postId, string content, User author, int score, string status, bool isDeleted, List<string> options, Dictionary<int, int> votes, List<Award> awards)
        {
            this.postId = postId;
            this.content = content;
            this.author = author;
            this.score = score;
            this.status = status;
            this.awards = awards;
            this.options = options;
            this.votes = votes;
            this.awards = awards;
        }

        public void addOption(string option)
        {
            options.Add(option);
        }
    }

    public class Vote
    {
        public int voteId { get; set; }
        public int userId { get; set; }
        public int pollId { get; set; }
        public string option { get; set; }

        public Vote(int voteId, int userId, int pollId, string option)
        {
            this.voteId = voteId;
            this.userId = userId;
            this.pollId = pollId;
            this.option = option;
        }
    }

    public class Award
    {
        public int awardId { get; set; }
        private enum AwardType
        {
            Bronze,
            Silver,
            Gold
        }
    }

    public interface IReport
    {
        int userId { get; set; }
        int reportId { get; set; }
        string message { get; set; }
    }

    public class PostReport : IReport
    {
        public int userId { get; set; }
        public int reportId { get; set; }
        public string message { get; set; }

        public PostReport(int userId, int reportId, string message)
        {
            this.userId = userId;
            this.reportId = reportId;
            this.message = message;
        }
    }

    public class CommentReport : IReport
    {
        public int userId { get; set; }
        public int reportId { get; set; }
        public string message { get; set; }

        public CommentReport(int userId, int reportId, string message)
        {
            this.userId = userId;
            this.reportId = reportId;
            this.message = message;
        }
    }

    public class MarketplaceItemReport : IReport
    {
        public int userId { get; set; }
        public int reportId { get; set; }
        public string message { get; set; }

        public MarketplaceItemReport(int userId, int reportId, string message)
        {
            this.userId = userId;
            this.reportId = reportId;
            this.message = message;
        }
    }

    public class JoinRequest
    {
        public int joinRequestID { get; set; }
        public int userId { get; set; }
        public string message { get; set; }
    }
}
