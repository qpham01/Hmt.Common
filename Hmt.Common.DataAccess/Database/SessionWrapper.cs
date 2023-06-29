using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public abstract class SessionWrapper<T, TKey> : ISessionWrapper<T, TKey>, IDisposable
    where T : class, IEntity<TKey>, ISoftDeletable
{
    protected readonly IDocumentSession _session;
    protected readonly IDatabaseQuery<T, TKey> _dbQuery;

    public IEventStoreWrapper Events => throw new NotImplementedException();

    public SessionWrapper(IDocumentSession session, IDatabaseQuery<T, TKey> dbQuery)
    {
        _session = session;
        _dbQuery = dbQuery;
    }

    public void Dispose()
    {
        _session.Dispose();
    }

    public IQueryable<T> Query()
    {
        return _session.Query<T>();
    }

    public async Task SaveChangesAsync()
    {
        await _session.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<T>> CustomQuery(IQueryable<T> query)
    {
        var result = await _dbQuery.QueryMany(query);
        return result;
    }

    public async Task<IReadOnlyList<T>> QueryAll()
    {
        var query = _session.Query<T>().Where(x => !x.IsDeleted);
        var result = await _dbQuery.QueryMany(query);
        return result;
    }

    public async Task<IReadOnlyList<T>> QueryPaged(int startIndex, int count)
    {
        var query = _session.Query<T>().Where(x => !x.IsDeleted).Skip(startIndex).Take(count);
        var result = await _dbQuery.QueryMany(query);
        return result;
    }

    public async Task Store(T entity)
    {
        _session.Store(entity);
        await _session.SaveChangesAsync();
    }

    public abstract Task DeleteAsync(T entity);
}
