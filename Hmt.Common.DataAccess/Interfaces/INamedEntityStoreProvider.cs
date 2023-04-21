namespace Hmt.Common.DataAccess.Interfaces;

public interface INamedEntityStoreProvider<T> : IDocumentStoreProvider where T : IHasName { }
