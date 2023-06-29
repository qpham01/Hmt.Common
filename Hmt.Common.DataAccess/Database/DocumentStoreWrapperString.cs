using Hmt.Common.DataAccess.Interfaces;

namespace Hmt.Common.DataAccess.Database;

public class DocumentStoreWrapperString<T> : DocumentStoreWrapper<T, string>
    where T : class, IEntity<string>, ISoftDeletable
{
    public DocumentStoreWrapperString(IDocumentStoreProvider storeProvider, ISchema schema)
        : base(storeProvider, schema) { }

    public override ISessionWrapper<T, string> OpenSession()
    {
        return new SessionWrapperStringKey<T>(_store.LightweightSession(), new DatabaseQuery<T, string>());
    }
}
