using Hmt.Common.DataAccess.Interfaces;

namespace Hmt.Common.DataAccess.Database;

public class EntityStoreStringKey<T> : EntityStoreAbstract<T, string> where T : class, IEntity<string>, ISoftDeletable
{
    public EntityStoreStringKey(IDocumentStoreWrapper<T, string> storeWrapper) : base(storeWrapper) { }

    public override async Task<T?> ReadAsync(string id)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            if (session == null)
                throw new InvalidOperationException("Session is null");
            var query = session.Query().Where(x => !x.IsDeleted && x.Id == id);
            var result = await session.CustomQuery(query);
            return result?.FirstOrDefault();
        }
    }

    public override Task ApplyEventAsync(string id, object @event)
    {
        throw new NotImplementedException();
    }
}
