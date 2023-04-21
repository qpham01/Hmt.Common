using Marten;

namespace Hmt.Common.DataAccess.Database;

public interface IDocumentStoreProvider
{
    IDocumentStore MakeStore();
}
