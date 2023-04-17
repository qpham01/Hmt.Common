using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class SessionWrapper : ISessionWrapper
{
    private readonly IDocumentSession _session;

    public SessionWrapper(IDocumentSession session)
    {
        _session = session;
    }

    public void Dispose()
    {
        _session.Dispose();
    }

    public IQueryable<T> Query<T>()
    {
        return _session.Query<T>();
    }

    public async Task<IReadOnlyList<T>> QueryAll<T>() where T : ISoftDeletable
    {
        return await _session.Query<T>().Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<T?> QuerySingleById<T, TKey>(Guid id) where T : IEntity<TKey>, ISoftDeletable
    {
        return await Query<T>().Where(x => !x.IsDeleted && x.Id!.Equals(id)).SingleAsync();
    }

    public async Task Store<T, TKey>(T entity) where T : IEntity<TKey>
    {
        _session.Store(entity);
        await _session.SaveChangesAsync();
    }

    public async Task DeleteAsync<T, TKey>(T entity) where T : IEntity<TKey>
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        _session.Delete(entity);
        await _session.SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _session.SaveChangesAsync();
    }

    public IEventStoreWrapper Events => new EventStoreWrapper(_session.Events);
}
