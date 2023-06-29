using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public abstract class DocumentStoreWrapper<T, TKey> : IDocumentStoreWrapper<T, TKey>
    where T : class, IEntity<TKey>, ISoftDeletable
{
    protected readonly IDocumentStore _store;

    public DocumentStoreWrapper(IDocumentStoreProvider storeProvider, ISchema schema)
    {
        _store = storeProvider.MakeStore(schema.Name);
    }

    public abstract ISessionWrapper<T, TKey> OpenSession();
}
