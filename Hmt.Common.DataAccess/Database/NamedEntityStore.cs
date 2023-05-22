using Hmt.Common.Core.Interfaces;
using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.DataAccess.Database;

public class NamedEntityStore<T> : EntityStoreGuidKey<T>, INamedEntityStore<T>
    where T : class, IEntity<Guid>, ISoftDeletable, IHasName, IDisposable
{
    public NamedEntityStore(INamedEntityStoreWrapper<T> storeWrapper) : base(storeWrapper) { }

    public virtual async Task<T?> ReadByName(string name)
    {
        using (var session = GetSession())
        {
            return await session.Query<T>().Where(x => x.Name == name && !x.IsDeleted).SingleOrDefaultAsync();
        }
    }

    public override async Task SoftDeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentNullException(nameof(id));

        using (var session = _storeWrapper.OpenSession())
        {
            var entity = await ReadAsync(id);
            if (entity != null)
            {
                entity.Name = $"{entity.Name}|{entity.Id}";
                entity.IsDeleted = true;
                await session.Store<T, Guid>(entity);
            }
        }
    }
}
