using Moderation.Model;

namespace Moderation.Entities
{
    public class Role : IHasID
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<Permission> Permissions { get; set; }
        public Role(string name, List<Permission>? permissions)
        {
            Id = Guid.NewGuid();
            Name = name;
            Permissions = permissions ?? [];
        }
        public Role(Guid id, string name)
        {
            Id = id;
            Name = name;
            Permissions = [];
        }
        public Role(Guid id, string name, List<Permission> permissions)
        {
            Id = id;
            Name = name;
            Permissions = permissions;
        }
    }

}