using Hmt.Common.DataAccess.Interfaces;
using Marten;

namespace Hmt.Common.IntegrationTests.DataAccess;

internal class TestDocumentStoreProvider : IDocumentStoreProvider
{
    private const string _connectionString = "host=localhost;database=hmtech;password=C3i?chJj&sU4;username=hmtech_dev";

    public IDocumentStore MakeStore(string schemaName)
    {
        var store = DocumentStore.For(_ =>
        {
            _.Connection(_connectionString);
            _.DatabaseSchemaName = schemaName;
        });
        return store;
    }
}
