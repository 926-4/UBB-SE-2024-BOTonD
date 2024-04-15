using Moderation.Model;

namespace Moderation.Repository
{
    internal class Repository<T> : IRepository<T> where T : IHasID
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

        public bool Add(Guid key, T value)
        {
            if (!data.ContainsKey(key)) return false;
            data.Add(key, value);
            return true;
        }

        public bool Contains(Guid key)
        {
            return data.ContainsKey(key);
        }

        public T? Get(Guid key)
        {
            var value = data.GetValueOrDefault(key);
            return value;
        }

        public IEnumerable<T> GetAll()
        {
            return data.Values;
        }

        public bool Remove(Guid key)
        {
            return data.Remove(key);
        }
        public bool Update(Guid key, T value)
        {
            if (!data.ContainsKey(key)) return false;
            data[key] = value;
            return true;
        }
    }
}
