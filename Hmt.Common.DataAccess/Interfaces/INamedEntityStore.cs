namespace Hmt.Common.DataAccess.Interfaces;

public interface INamedEntityStore<T> : IEntityStore<T, Guid> where T : class, IEntity<Guid>, ISoftDeletable, IHasName
{
    Task<T?> ReadByName(string name);
}
