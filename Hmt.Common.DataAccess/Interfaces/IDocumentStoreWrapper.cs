namespace Hmt.Common.DataAccess.Interfaces;

public interface IDocumentStoreWrapper<T, TKey> where T : IEntity<TKey>, ISoftDeletable
{
    ISessionWrapper<T, TKey> OpenSession();
}
