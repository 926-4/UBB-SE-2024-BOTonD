using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    internal class Repository<T> : IRepository<T>
    {
        private Dictionary<Guid, T> data;
        public Repository(Dictionary<Guid, T> data)
        {
            data = new Dictionary<Guid, T>();
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
            if (data.ContainsKey(key)) return data[key];
            return default;
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
