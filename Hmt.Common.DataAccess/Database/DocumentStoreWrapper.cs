using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class DocumentStoreWrapper : IDocumentStoreWrapper
{
    private readonly IDocumentStore _store;

    public DocumentStoreWrapper(IDocumentStoreProvider storeProvider, ISchema schema)
    {
        _store = storeProvider.MakeStore(schema.Name);
    }

    public ISessionWrapper OpenSession()
    {
        return new SessionWrapper(_store.OpenSession());
    }
}
