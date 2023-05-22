using Hmt.Common.Core.Interfaces;

namespace Hmt.Common.DataAccess.Interfaces;

public interface INamedEntityStoreWrapper<T> : IDocumentStoreWrapper where T : IHasName { }
