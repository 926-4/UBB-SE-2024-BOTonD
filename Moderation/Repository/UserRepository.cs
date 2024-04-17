using Moderation.Model;
using Moderation.Entities;
using Moderation.DbEndpoints;

namespace Moderation.Repository
{
    public class UserRepository : Repository<User> 
    {
        public UserRepository(Dictionary<Guid, User> data) : base(data) { }

        public UserRepository() : base() { }

        public override bool Add(Guid key, User value)
        {
            UserEndpoints.CreateUser(value);
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
