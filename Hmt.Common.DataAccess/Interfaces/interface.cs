using Hmt.Common.DataAccess.Database;
using Marten;

namespace Hmt.Common.DataAccess.Interfaces;

public interface IDocumentStoreWrapper
{
    ISessionWrapper OpenSession();
}
namespace Hmt.Common.DataAccess.Interfaces;

public interface IEntity<T>
{
    T Id { get; set; }
}
using Hmt.Common.DataAccess.Interfaces;

public interface IEntityStore<T, TKey> where T : class, IEntity<TKey>, ISoftDeletable
{
    Task<T> CreateAsync(T entity);
    Task<T?> ReadAsync(TKey id);
    Task UpdateAsync(T entity);
    Task SoftDeleteAsync(TKey id);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetPageAsync(int startIndex, int count);
    Task ApplyEventAsync(TKey id, object @event);
}
namespace Hmt.Common.DataAccess.Interfaces;

public interface IEventStoreWrapper
{
    void Append(Guid id, object @event);
}
namespace Hmt.Common.DataAccess.Interfaces;

public interface ISessionWrapper : IDisposable
{
    IQueryable<T> Query<T>();
    void Store<T, TKey>(T entity) where T : IEntity<TKey>;
    Task DeleteAsync<T>(T id);
    Task SaveChangesAsync();
    IEventStoreWrapper Events { get; }
}
namespace Hmt.Common.DataAccess.Interfaces;

public interface ISoftDeletable
{
    bool IsDeleted { get; set; }
}
