using Hmt.Common.DataAccess.Interfaces;

namespace Hmt.Common.DataAccess.Database;

public class DocumentStoreWrapperLong<T> : DocumentStoreWrapper<T, long> where T : class, IEntity<long>, ISoftDeletable
{
    public DocumentStoreWrapperLong(IDocumentStoreProvider storeProvider, ISchema schema) : base(storeProvider, schema)
    { }

    public override ISessionWrapper<T, long> OpenSession()
    {
        return new SessionWrapperLongKey<T>(_store.LightweightSession(), new DatabaseQuery<T, long>());
    }
}
