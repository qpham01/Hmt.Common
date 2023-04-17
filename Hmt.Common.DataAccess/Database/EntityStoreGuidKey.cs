using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class EntityStoreGuidKey<T> : EntityStoreAbstract<T, Guid> where T : class, IEntity<Guid>, ISoftDeletable
{
    public EntityStoreGuidKey(IDocumentStoreWrapper storeWrapper) : base(storeWrapper) { }

    public override async Task<T?> ReadAsync(Guid id)
    {
        using (var session = _storeWrapper.OpenSession())
        {
            if (session == null)
                throw new InvalidOperationException("Session is null");
            var query = session.Query<T>().Where(x => !x.IsDeleted && x.Id.Equals(id));
            return await query.SingleAsync();
        }
    }

    public override async Task ApplyEventAsync(Guid id, object @event)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));
        if (@event == null)
            throw new ArgumentNullException(nameof(@event));

        using (var session = _storeWrapper.OpenSession())
        {
            session.Events.Append(id, @event);
            await session.SaveChangesAsync();
        }
    }
}
