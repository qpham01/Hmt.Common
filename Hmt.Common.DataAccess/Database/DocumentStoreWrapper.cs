using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class DocumentStoreWrapper : IDocumentStoreWrapper
{
    private readonly IDocumentStore _store;

    public DocumentStoreWrapper(IDocumentStoreProvider storeProvider)
    {
        _store = storeProvider.MakeStore();
    }

    public ISessionWrapper OpenSession()
    {
        return new SessionWrapper(_store.OpenSession());
    }
}
