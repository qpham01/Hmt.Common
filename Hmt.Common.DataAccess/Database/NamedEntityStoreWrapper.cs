using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class NamedEntityStoreWrapper<T> : INamedEntityStoreWrapper<T> where T : IHasName, ISoftDeletable
{
    IDocumentStore _store;

    public NamedEntityStoreWrapper(INamedEntityStoreProvider<T> storeProvider, ISchema schema)
    {
        _store = storeProvider.MakeStore(schema.Name);
    }

    public ISessionWrapper OpenSession()
    {
        return new SessionWrapper(_store.OpenSession());
    }
}
