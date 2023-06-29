using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.DataAccess.Interfaces;

public interface INamedEntityStore<T> : IEntityStore<T, string>
    where T : class, IEntity<string>, ISoftDeletable, IHasName
{
    Task<T?> ReadByName(string name);
}
