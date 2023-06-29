using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class SessionWrapperLongKey<T> : SessionWrapper<T, long> where T : class, IEntity<long>, ISoftDeletable
{
    public SessionWrapperLongKey(IDocumentSession session, IDatabaseQuery<T, long> dbQuery) : base(session, dbQuery) { }

    public override async Task DeleteAsync(T entity)
    {
        _session.Delete(entity);
        await _session.SaveChangesAsync();
    }
}
