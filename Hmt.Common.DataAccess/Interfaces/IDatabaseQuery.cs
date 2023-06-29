namespace Hmt.Common.DataAccess.Interfaces;

public interface IDatabaseQuery<E, T> where E : class, IEntity<T>, ISoftDeletable
{
    Task<IReadOnlyList<E>> QueryMany(IQueryable<E> query);
    Task<E?> QueryOne(IQueryable<E> query);
}
