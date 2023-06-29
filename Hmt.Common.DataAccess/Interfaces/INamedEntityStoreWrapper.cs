using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.DataAccess.Interfaces;

public interface INamedEntityStoreWrapper<T, TKey> : IDocumentStoreWrapper<T, TKey>
    where T : IHasName, IEntity<TKey>, ISoftDeletable { }
