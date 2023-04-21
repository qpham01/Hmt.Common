using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class NamedEntityStoreWrapper<T> : INamedEntityStoreWrapper<T> where T : IHasName
{
    IDocumentStore _store;

    public NamedEntityStoreWrapper(INamedEntityStoreProvider<T> storeProvider)
    {
        _store = storeProvider.MakeStore();
    }

    public ISessionWrapper OpenSession()
    {
        return new SessionWrapper(_store.OpenSession());
    }
}
