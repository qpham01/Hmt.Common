namespace Hmt.Common.DataAccess.Interfaces;

public interface ISessionWrapperTyped<T, TKey> : IDisposable where T : IEntity<TKey>, ISoftDeletable
{
    IQueryable<T> Query();
    Task<IReadOnlyList<T>> QueryAll();
    Task<T?> QuerySingleById(TKey id);
    Task Store(T entity);
    Task SaveChangesAsync();
    Task DeleteAsync(T id);
    IEventStoreWrapper Events { get; }
}
