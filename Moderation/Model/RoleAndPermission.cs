namespace Moderation.Entities
{
    public class RoleAndPermission : IHasID
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }
        public RoleAndPermission(string name, List<Permission>? permissions)
        {
            Id = Guid.NewGuid();
            Name = name;
            Permissions = permissions ?? [];
        }
        public RoleAndPermission(Guid id, string name)
        {
            Id = id;
            Name = name;
            Permissions = [];
        }
        public RoleAndPermission(Guid id, string name, List<Permission> permissions)
        {
            Id = id;
            Name = name;
            Permissions = permissions;
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