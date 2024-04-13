namespace Moderation.Repository
{
    public interface IIDInterface
    {
        Guid ID { get; set; }
    }
    public interface IRepository<T> where T : IIDInterface
    {
        bool Add(Guid key, T value);
        bool Remove(Guid key);
        T? Get(Guid key);
        IEnumerable<T> GetAll();
        bool Contains(Guid key);
        bool Update(Guid key, T value);
    }
}
