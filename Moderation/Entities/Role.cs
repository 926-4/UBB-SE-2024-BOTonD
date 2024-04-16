using Moderation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class Role(Guid roleId, string name) : IHasID
    {
        public Guid Id { get; set; } = roleId;
        public string Name { get; set; } = name;
        public List<Permission> Permissions { get; set; } = [];
    }

    public enum Permission
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
}
