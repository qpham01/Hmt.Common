using Hmt.Common.DataAccess.Interfaces;

public interface IEntityStore<T, TKey> where T : class, IEntity<TKey>, ISoftDeletable
{
    Task<T> CreateAsync(T entity);
    Task<T?> ReadAsync(TKey id);
    Task UpdateAsync(T entity);
    Task SoftDeleteAsync(TKey id);
    Task DeleteAsync(T entity);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetPageAsync(int startIndex, int count);
    Task ApplyEventAsync(TKey id, object @event);
}
