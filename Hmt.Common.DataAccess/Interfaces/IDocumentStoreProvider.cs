using Marten;

namespace Hmt.Common.DataAccess.Interfaces;

public interface IDocumentStoreProvider
{
    IDocumentStore MakeStore(string schemaName);
}
