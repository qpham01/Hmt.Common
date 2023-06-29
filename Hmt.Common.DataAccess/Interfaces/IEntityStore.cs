using Hmt.Common.DataAccess.Interfaces;

public interface IEntityStore<T, TKey> where T : class, IEntity<TKey>, ISoftDeletable
{
    Task<T> CreateAsync(T entity);
    Task<IReadOnlyList<T>> ReadAllAsync();
    Task<IReadOnlyList<T>> ReadPageAsync(int startIndex, int count);
    Task<T?> ReadAsync(TKey id);
    Task UpdateAsync(T entity);
    Task SoftDeleteAsync(TKey id);
    Task DeleteAsync(T entity);
    Task ApplyEventAsync(TKey id, object @event);
    ISessionWrapper<T, TKey> GetSession();
}
