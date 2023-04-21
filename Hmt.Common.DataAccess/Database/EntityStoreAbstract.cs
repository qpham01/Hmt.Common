using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public abstract class EntityStoreAbstract<T, TKey> : IEntityStore<T, TKey>
    where T : class, IEntity<TKey>, ISoftDeletable
{
    protected readonly IDocumentStoreWrapper _storeWrapper;

    public abstract Task<T?> ReadAsync(TKey id);

    public abstract Task ApplyEventAsync(TKey id, object @event);

    public EntityStoreAbstract(IDocumentStoreWrapper storeWrapper)
    {
        _storeWrapper = storeWrapper;
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            await session.Store<T, TKey>(entity);
        }
        return entity;
    }

    public virtual async Task UpdateAsync(T entity)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            await session.Store<T, TKey>(entity);
        }
    }

    public virtual async Task SoftDeleteAsync(TKey id)
    {
        if (typeof(TKey).IsClass && id == null)
            throw new ArgumentNullException(nameof(id));

        using (var session = _storeWrapper.OpenSession())
        {
            var entity = await ReadAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await session.Store<T, TKey>(entity);
            }
        }
    }

    public virtual async Task<IReadOnlyList<T>> ReadAllAsync()
    {
        using (var session = _storeWrapper.OpenSession())
        {
            return await session.QueryAll<T>();
        }
    }

    public virtual async Task<IReadOnlyList<T>> ReadPageAsync(int startIndex, int count)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            return await session.Query<T>().Where(x => !x.IsDeleted).Skip(startIndex).Take(count).ToListAsync();
        }
    }

    public virtual ISessionWrapper GetSession()
    {
        return _storeWrapper.OpenSession();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            if (session == null)
                throw new InvalidOperationException("Session is null");
            await session.DeleteAsync<T, TKey>(entity);
        }
    }
}
