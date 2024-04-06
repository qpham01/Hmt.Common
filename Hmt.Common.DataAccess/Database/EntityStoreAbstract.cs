using Hmt.Common.DataAccess.Interfaces;

namespace Hmt.Common.DataAccess.Database;

public abstract class EntityStoreAbstract<T, TKey> : IEntityStore<T, TKey>
    where T : class, IEntity<TKey>, ISoftDeletable
{
    protected readonly IDocumentStoreWrapper<T, TKey> _storeWrapper;

    public abstract Task<T?> ReadAsync(TKey id);

    public abstract Task ApplyEventAsync(TKey id, object @event);

    public EntityStoreAbstract(IDocumentStoreWrapper<T, TKey> storeWrapper)
    {
        _storeWrapper = storeWrapper;
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            await session.Store(entity);
            return entity;
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
                await session.Store(entity);
            }
        }
    }

    public virtual async Task<IReadOnlyList<T>> ReadAllAsync()
    {
        using (var session = _storeWrapper.OpenSession())
        {
            return await session.QueryAll();
        }
    }

    public virtual async Task<IReadOnlyList<T>> ReadPageAsync(int startIndex, int count)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            var query = session.Query().Where(x => !x.IsDeleted).Skip(startIndex).Take(count);
            var result = await session.CustomQuery(query);
            return result;
        }
    }

    public virtual async Task DeleteAsync(T entity)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            if (session == null)
                throw new ArgumentNullException(nameof(entity));
            await session.DeleteAsync(entity);
        }
    }

    public ISessionWrapper<T, TKey> GetSession()
    {
        throw new NotImplementedException();
    }
}
