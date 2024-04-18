using Moderation.DbEndpoints;
using Moderation.Model;

namespace Moderation.Repository
{
    internal class GroupRepository : Repository<Group>
    {
        public GroupRepository(Dictionary<Guid, Group> data) : base(data) { }

        public GroupRepository() : base() { }

        public override bool Add(Guid key, Group value)
        {
            GroupEndpoints.CreateGroup(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return GroupEndpoints.ReadAllGroups().Exists(u => u.Id == key);
        }

        public override Group? Get(Guid key)
        {
            return GroupEndpoints.ReadAllGroups().Find(u => u.Id == key);
        }

        public override IEnumerable<Group> GetAll()
        {
            return GroupEndpoints.ReadAllGroups();
        }

        public override bool Remove(Guid key)
        {
            GroupEndpoints.DeleteGroup(key);
            return true;
        }

        public override bool Update(Guid key, Group value)
        {
            GroupEndpoints.UpdateGroup(value);
            return true;
        }
        public Guid? GetGuidByName(string name)
        {
            return GroupEndpoints.ReadAllGroups().Find(u => u.Name == name)?.Id;
        }
    }
}
