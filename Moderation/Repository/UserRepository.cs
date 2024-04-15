using Moderation.Model;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class UserRepository : Repository<User> 
    {
        public UserRepository(Dictionary<Guid, User> data) : base(data) { }
        public UserRepository(): base() { }
        public Guid? GetGuidByName(string name)
        {
            var user = data.Values.FirstOrDefault(u => u.Username == name);
            return user?.Id;
        }
    }
}
