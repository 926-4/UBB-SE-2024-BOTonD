using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Repository
{
    public interface IRepository<T>
    {
        bool Add(Guid key, T value);
        bool Remove(Guid key);
        T? Get(Guid key);
        IEnumerable<T> GetAll();
        bool Contains(Guid key);
        bool Update(Guid key, T value);
    }
}
