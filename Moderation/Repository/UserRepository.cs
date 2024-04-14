using Moderation.Model;
using Moderation.Entities;

namespace Moderation.Repository
{
    public class UserRepository : IRepository<User> 
    {
        private readonly Dictionary<Guid, User> data;
        public UserRepository(Dictionary<Guid, User> data)
        {
            this.data = data;
        }
        public UserRepository()
        {
            this.data = [];
        }

        public bool Add(Guid key, User value)
        {
            if (!data.ContainsKey(key)) return false;
            data.Add(key, value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return data.ContainsKey(key);
        }

        public User? Get(Guid key)
        {
            var value = data.GetValueOrDefault(key);
            return value;
        }

        public IEnumerable<User> GetAll()
        {
            return data.Values;
        }

        public bool Remove(Guid key)
        {
            return data.Remove(key);
        }
        public bool Update(Guid key, User value)
        {
            if (!data.ContainsKey(key)) return false;
            data[key] = value;
            return true;
        }
        public Guid? GetGuidByName(string name)
        {
            var user = data.Values.FirstOrDefault(u => u.username == name);
            return user?.Id;
        }
    }
}
