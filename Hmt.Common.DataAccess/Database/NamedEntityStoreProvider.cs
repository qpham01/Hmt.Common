using Hmt.Common.DataAccess.Interfaces;
using Marten;
using Marten.Schema;

namespace Hmt.Common.DataAccess.Database;

public class NamedEntityStoreProvider<T> : INamedEntityStoreProvider<T> where T : IHasName
{
    private readonly string _connectionString =
        "host=localhost;database=hmtech;password=C3i?chJj&sU4;username=hmtech_dev";

    public IDocumentStore MakeStore(string schemaName)
    {
        var store = DocumentStore.For(_ =>
        {
            _.Connection(_connectionString);
            _.DatabaseSchemaName = schemaName;
            _.Schema
                .For<T>()
                .Index(
                    x => x.Name,
                    x =>
                    {
                        x.Casing = ComputedIndex.Casings.Lower;
                        x.Name = $"idx_{typeof(T).Name}_name";
                        x.IsUnique = true;
                        x.IsConcurrent = true;
                    }
                );
        });
        return store;
    }
}
