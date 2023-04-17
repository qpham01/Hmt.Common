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

    public IQueryable<T> Query<T>()
    {
        return _session.Query<T>();
    }

    public void Store<T, TKey>(T entity) where T : IEntity<TKey>
    {
        _session.Store(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _session.SaveChangesAsync();
    }

    public void Dispose()
    {
        _session.Dispose();
    }

    public async Task DeleteAsync<T>(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        _session.Delete(entity);
        await _session.SaveChangesAsync();
    }

    public IEventStoreWrapper Events => new EventStoreWrapper(_session.Events);
}
