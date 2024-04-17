using Moderation.Model;

namespace Moderation.Repository
{
    public class Repository<T> : IRepository<T> where T : IHasID
    {

        protected readonly Dictionary<Guid, T> data;
        public Repository(Dictionary<Guid, T> data)
        {
            this.data = data;
        }
        public Repository()
        {
            this.data = [];
        }

        public virtual bool Add(Guid key, T value)
        {
            if (!data.ContainsKey(key)) return false;
            data.Add(key, value);
            return true;
        }

        public virtual bool Contains(Guid key)
        {
            return data.ContainsKey(key);
        }

        public virtual T? Get(Guid key)
        {
            var value = data.GetValueOrDefault(key);
            return value;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return data.Values;
        }

        public virtual bool Remove(Guid key)
        {
            return data.Remove(key);
        }
        public virtual bool Update(Guid key, T value)
        {
            if (!data.ContainsKey(key)) return false;
            data[key] = value;
            return true;
        }
    }
}