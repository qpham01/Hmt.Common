namespace Hmt.Common.DataAccess.Interfaces;

public interface ISessionWrapper : IDisposable
{
    IQueryable<T> Query<T>();
    Task<IReadOnlyList<T>> QueryAll<T>() where T : ISoftDeletable;
    Task Store<T, TKey>(T entity) where T : IEntity<TKey>;
    Task DeleteAsync<T, TKey>(T id) where T : IEntity<TKey>;
    Task SaveChangesAsync();
    IEventStoreWrapper Events { get; }
}
