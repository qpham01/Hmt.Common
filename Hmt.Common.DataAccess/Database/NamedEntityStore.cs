using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class NamedEntityStore<T> : EntityStoreGuidKey<T>, INamedEntityStore<T>
    where T : class, IEntity<Guid>, ISoftDeletable, IHasName
{
    public NamedEntityStore(INamedEntityStoreWrapper<T> storeWrapper) : base(storeWrapper) { }

    public async Task<T?> ReadByName(string name)
    {
        using (var session = GetSession())
        {
            return await session.Query<T>().Where(x => x.Name == name).SingleOrDefaultAsync();
        }
    }
}
