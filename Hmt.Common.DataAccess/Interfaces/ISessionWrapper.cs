namespace Hmt.Common.DataAccess.Interfaces;

public interface ISessionWrapper<T, TKey> : IDisposable where T : IEntity<TKey>, ISoftDeletable
{
    IQueryable<T> Query();
    Task<IReadOnlyList<T>> CustomQuery(IQueryable<T> query);
    Task<IReadOnlyList<T>> QueryAll();
    Task<IReadOnlyList<T>> QueryPaged(int start, int count);
    Task Store(T entity);
    Task SaveChangesAsync();
    Task DeleteAsync(T entity);
    IEventStoreWrapper Events { get; }
}
