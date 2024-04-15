using Moderation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class Role: IHasID
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }

        public Role(Guid roleId, string name)
        {
            this.Id = roleId;
            this.Name = name;
            this.Permissions = new List<Permission>();
        }
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
