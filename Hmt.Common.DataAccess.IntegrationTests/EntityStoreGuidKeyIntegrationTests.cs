using FluentAssertions;
using Hmt.Common.DataAccess.Interfaces;
using Hmt.Common.DataAccess.Database;
using Marten;

namespace Hmt.Common.DataAccess.IntegrationTests;

public class TestEntityGuid : IEntity<Guid>, ISoftDeletable
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}

public class EntityStoreGuidKeyIntegrationTests
{
    private IEntityStore<TestEntityGuid, Guid> _sut;
    private IDocumentStore _store;
    private IDocumentStoreWrapper _storeWrapper;
    private const string _connectionString =
        "host=localhost;database=hmtech;password=C3i?chJj&sU4;username=hmtech_dev";

    private TestEntityGuid _testEntity1;
    private TestEntityGuid _testEntity2;
    private TestEntityGuid _testEntity3;

    [SetUp]
    public async Task Setup()
    {
        _store = DocumentStore.For(_connectionString);
        _storeWrapper = new DocumentStoreWrapper(_store);
        _sut = new EntityStoreGuidKey<TestEntityGuid>(_storeWrapper);
        _testEntity1 = new TestEntityGuid { Id = Guid.NewGuid(), Name = "Test Entity 1" };
        _testEntity2 = new TestEntityGuid { Id = Guid.NewGuid(), Name = "Test Entity 2" };
        _testEntity3 = new TestEntityGuid { Id = Guid.NewGuid(), Name = "Test Entity 3" };
        await _sut.CreateAsync(_testEntity1);
        await _sut.CreateAsync(_testEntity2);
        await _sut.CreateAsync(_testEntity3);
    }

    [TearDown]
    public async Task Teardown()
    {
        await _sut.DeleteAsync(_testEntity1);
        await _sut.DeleteAsync(_testEntity2);
        await _sut.DeleteAsync(_testEntity3);
        var allEntities = await _sut.GetAllAsync();
        allEntities.Count().Should().Be(0);
        _store.Dispose();
    }

    [Test]
    public async Task Guid_sut_CRUD_Operations_Should_Work_Correctly()
    {
        // Arrange
        var testEntity = new TestEntityGuid { Id = Guid.NewGuid(), Name = "Test Entity" };

        // Act - Create
        var createdEntity = await _sut.CreateAsync(testEntity);

        // Assert - Create
        createdEntity.Should().NotBeNull();
        createdEntity.Id.Should().Be(testEntity.Id);
        createdEntity.Name.Should().Be(testEntity.Name);

        // Act - Read
        var readEntity = await _sut.ReadAsync(testEntity.Id);

        // Assert - Read
        readEntity.Should().NotBeNull();
        readEntity!.Id.Should().Be(testEntity.Id);
        readEntity.Name.Should().Be(testEntity.Name);

        // Act - Update
        readEntity.Name = "Updated Test Entity";
        await _sut.UpdateAsync(readEntity);

        // Assert - Update
        using (var session = _storeWrapper.OpenSession())
        {
            var updatedEntity = await session
                .Query<TestEntityGuid>()
                .FirstOrDefaultAsync(x => x.Id == testEntity.Id);
            updatedEntity.Should().NotBeNull();
            updatedEntity!.Name.Should().Be("Updated Test Entity");
        }

        // Act - Soft Delete
        await _sut.SoftDeleteAsync(testEntity.Id);

        // Assert - Soft Delete
        using (var session = _storeWrapper.OpenSession())
        {
            var softDeletedEntity = await session
                .Query<TestEntityGuid>()
                .FirstOrDefaultAsync(x => x.Id == testEntity.Id);
            softDeletedEntity.Should().NotBeNull();
            softDeletedEntity!.IsDeleted.Should().BeTrue();
        }

        // Act - Hard Delete
        await _sut.DeleteAsync(testEntity);

        // Assert - Delete
        using (var session = _storeWrapper.OpenSession())
        {
            var deletedEntity = await session
                .Query<TestEntityGuid>()
                .FirstOrDefaultAsync(x => x.Id == testEntity.Id);
            deletedEntity.Should().BeNull();
        }
    }

    [Test]
    public async Task Guid_sut_GetAll_Operation_Should_Work_Correctly()
    {
        // Act
        var allEntities = await _sut.GetAllAsync();

        // Assert
        allEntities.Should().NotBeEmpty();
        allEntities.Count().Should().BeGreaterThanOrEqualTo(3);
    }

    [Test]
    public async Task Guid_sut_GetPage_Operation_Should_Work_Correctly()
    {
        // Act
        var firstPageEntities = await _sut.GetPageAsync(0, 2);
        var secondPageEntities = await _sut.GetPageAsync(2, 2);

        // Assert
        firstPageEntities.Should().NotBeEmpty().And.HaveCount(2);
        secondPageEntities.Should().NotBeEmpty().And.HaveCount(1);

        firstPageEntities.Should().Contain(e => e.Name == _testEntity1.Name);
        firstPageEntities.Should().Contain(e => e.Name == _testEntity2.Name);
        secondPageEntities.Should().Contain(e => e.Name == _testEntity3.Name);
    }
}