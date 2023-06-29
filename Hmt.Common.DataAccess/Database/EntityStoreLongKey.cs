using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class EntityStoreLongKey<T> : EntityStoreAbstract<T, long>
    where T : class, IEntity<long>, ISoftDeletable, IDisposable
{
    public EntityStoreLongKey(IDocumentStoreWrapper<T, long> storeWrapper) : base(storeWrapper) { }

    public override async Task<T?> ReadAsync(long id)
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

    public override Task ApplyEventAsync(long id, object @event)
    {
        throw new NotImplementedException("Events not implemented for long keys.");
    }
}
