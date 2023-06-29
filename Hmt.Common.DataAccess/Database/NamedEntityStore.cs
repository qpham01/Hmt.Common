using Hmt.Common.Core.Interfaces;
using Hmt.Common.DataAccess.Interfaces;

namespace Hmt.Common.DataAccess.Database;

public class NamedEntityStore<T> : EntityStoreStringKey<T>, INamedEntityStore<T>
    where T : class, IEntity<string>, ISoftDeletable, IHasName
{
    public NamedEntityStore(INamedEntityStoreWrapper<T, string> storeWrapper) : base(storeWrapper) { }

    public virtual async Task<T?> ReadByName(string name)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            var query = session.Query().Where(x => x.Name == name && !x.IsDeleted);
            var result = await session.CustomQuery(query);
            return result.FirstOrDefault();
        }
    }
}
