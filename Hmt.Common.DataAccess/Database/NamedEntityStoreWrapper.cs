using Hmt.Common.Core.Interfaces;
using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public abstract class NamedEntityStoreWrapper<T, TKey> : INamedEntityStoreWrapper<T, TKey>
    where T : class, IHasName, IEntity<TKey>, ISoftDeletable
{
    IDocumentStore _store;
    IDatabaseQuery<T, TKey> _dbQuery;

    public NamedEntityStoreWrapper(
        INamedEntityStoreProvider<T> storeProvider,
        IDatabaseQuery<T, TKey> dbQuery,
        ISchema schema
    )
    {
        _store = storeProvider.MakeStore(schema.Name);
        _dbQuery = dbQuery;
    }

    public abstract ISessionWrapper<T, TKey> OpenSession();
}
