namespace Hmt.Common.DataAccess.Interfaces;

public interface ISessionWrapper : IDisposable
{
    IQueryable<T> Query<T>();
    void Store<T, TKey>(T entity) where T : IEntity<TKey>;
    Task DeleteAsync<T>(T id);
    Task SaveChangesAsync();
    IEventStoreWrapper Events { get; }
}
