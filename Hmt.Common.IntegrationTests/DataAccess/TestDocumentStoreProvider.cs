using Hmt.Common.DataAccess.Database;
using Marten;

namespace Hmt.Common.IntegrationTests.DataAccess;

internal class TestDocumentStoreProvider : IDocumentStoreProvider
{
    private const string _connectionString = "host=localhost;database=hmtech;password=C3i?chJj&sU4;username=hmtech_dev";

    public IDocumentStore MakeStore()
    {
        var store = DocumentStore.For(_connectionString);
        return store;
    }
}
