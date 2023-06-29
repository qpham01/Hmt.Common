using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class SessionWrapperStringKey<T> : SessionWrapper<T, string> where T : class, IEntity<string>, ISoftDeletable
{
    public SessionWrapperStringKey(IDocumentSession session, IDatabaseQuery<T, string> dbQuery) : base(session, dbQuery)
    { }

    public override async Task DeleteAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException(nameof(entity));
        _session.Delete(entity);
        await _session.SaveChangesAsync();
    }
}
