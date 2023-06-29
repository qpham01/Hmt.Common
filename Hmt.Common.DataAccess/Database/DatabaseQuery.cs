using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class DatabaseQuery<E, T> : IDatabaseQuery<E, T> where E : class, IEntity<T>, ISoftDeletable
{
    public async Task<IReadOnlyList<E>> QueryMany(IQueryable<E> query)
    {
        var result = await query.ToListAsync();
        return result;
    }

    public async Task<E?> QueryOne(IQueryable<E> query)
    {
        var result = await query.FirstOrDefaultAsync();
        return result;
    }
}
