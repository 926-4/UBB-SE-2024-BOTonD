using Moderation.DbEndpoints;
using Moderation.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public class GroupUserRepository : Repository<GroupUser>
    {
        public GroupUserRepository(Dictionary<Guid, GroupUser> data) : base(data) { }

        public GroupUserRepository() : base() { }

        public override bool Add(Guid key, GroupUser value)
        {
            DbEndpoints.GroupUserEndpoints.CreateGroupUser(value);
            return true;
        }

        public override bool Contains(Guid key)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Exists(u => u.Id == key);
        }

        public override GroupUser? Get(Guid key)
        {
            return GroupUserEndpoints.ReadAllGroupUsers().Find(u => u.Id == key);
        }

        public override IEnumerable<GroupUser> GetAll()
        {
            return GroupUserEndpoints.ReadAllGroupUsers();
        }

        public override bool Remove(Guid key)
        {
            GroupUserEndpoints.DeleteGroupUser(key);
            return true;
        }

        public override bool Update(Guid key, GroupUser value)
        {
            GroupUserEndpoints.UpdateGroupUser(value);
            return true;
        }
    }
}
