using Moderation.DbEndpoints;
using Moderation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return UserEndpoints.ReadAllUsers().Exists(u => u.Id == key);
        }

        public override User? Get(Guid key)
        {
            return UserEndpoints.ReadAllUsers().Find(u => u.Id == key);
        }

        public override IEnumerable<User> GetAll()
        {
            return UserEndpoints.ReadAllUsers();
        }

        public override bool Remove(Guid key)
        {
            UserEndpoints.DeleteUser(key);
            return true;
        }

        public override bool Update(Guid key, User value)
        {
            UserEndpoints.UpdateUser(value);
            return true;
        }
        public Guid? GetGuidByName(string name)
        {
            return UserEndpoints.ReadAllUsers().Find(u => u.Username == name)?.Id;
        }
    }
}
