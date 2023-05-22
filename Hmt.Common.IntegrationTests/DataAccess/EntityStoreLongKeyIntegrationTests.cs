using FluentAssertions;
using Hmt.Common.DataAccess.Interfaces;
using Hmt.Common.DataAccess.Database;
using Marten;

namespace Hmt.Common.IntegrationTests.DataAccess;

public class TestEntityLong : IEntity<long>, ISoftDeletable, IDisposable
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public void Dispose() { }
}

public class EntityStoreLongKeyIntegrationTests
{
    private IEntityStore<TestEntityLong, long> _sut;
    private IDocumentStoreWrapper _storeWrapper;

    private TestEntityLong _testEntity1;
    private TestEntityLong _testEntity2;
    private TestEntityLong _testEntity3;

    [OneTimeSetUp]
    public async Task OneTimeSetup()
    {
        _storeWrapper = new DocumentStoreWrapper(new TestDocumentStoreProvider(), new TestSchema { Name = "public" });
        _sut = new EntityStoreLongKey<TestEntityLong>(_storeWrapper);
        _testEntity1 = new TestEntityLong { Name = "Test Entity 1" };
        _testEntity2 = new TestEntityLong { Name = "Test Entity 2" };
        _testEntity3 = new TestEntityLong { Name = "Test Entity 3" };
        await _sut.CreateAsync(_testEntity1);
        await _sut.CreateAsync(_testEntity2);
        await _sut.CreateAsync(_testEntity3);
    }

    [OneTimeTearDown]
    public async Task OneTimeTeardown()
    {
        await _sut.DeleteAsync(_testEntity1);
        await _sut.DeleteAsync(_testEntity2);
        await _sut.DeleteAsync(_testEntity3);
        var allEntities = await _sut.ReadAllAsync();
        allEntities.Count().Should().Be(0);
    }

    [Test]
    public async Task EntityStoreLongKey_CRUD_Operations_Should_Work_Correctly()
    {
        // Arrange
        var testEntity = new TestEntityLong { Name = "Test Entity" };

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
            var updatedEntity = await session.Query<TestEntityLong>().FirstOrDefaultAsync(x => x.Id == testEntity.Id);
            updatedEntity.Should().NotBeNull();
            updatedEntity!.Name.Should().Be("Updated Test Entity");
        }

        // Act - Soft Delete
        await _sut.SoftDeleteAsync(testEntity.Id);

        // Assert - Soft Delete
        using (var session = _storeWrapper.OpenSession())
        {
            var softDeletedEntity = await session
                .Query<TestEntityLong>()
                .FirstOrDefaultAsync(x => x.Id == testEntity.Id);
            softDeletedEntity.Should().NotBeNull();
            softDeletedEntity!.IsDeleted.Should().BeTrue();
        }

        // Act - Hard Delete
        await _sut.DeleteAsync(testEntity);

        // Assert - Delete
        using (var session = _storeWrapper.OpenSession())
        {
            var deletedEntity = await session.Query<TestEntityLong>().FirstOrDefaultAsync(x => x.Id == testEntity.Id);
            deletedEntity.Should().BeNull();
        }
    }

    [Test]
    public async Task EntityStoreLongKey_ReadAll_Operation_Should_Work_Correctly()
    {
        // Act
        var allEntities = await _sut.ReadAllAsync();

        // Assert
        allEntities.Should().NotBeEmpty();
        allEntities.Count().Should().BeGreaterThanOrEqualTo(3);
    }

    [Test]
    public async Task EntityStoreLongKey_ReadPage_Operation_Should_Work_Correctly()
    {
        // Act
        var firstPageEntities = await _sut.ReadPageAsync(0, 2);
        var secondPageEntities = await _sut.ReadPageAsync(2, 2);

        // Assert
        firstPageEntities.Should().NotBeEmpty().And.HaveCount(2);
        secondPageEntities.Should().NotBeEmpty().And.HaveCount(1);

        firstPageEntities.Should().Contain(e => e.Name == _testEntity1.Name);
        firstPageEntities.Should().Contain(e => e.Name == _testEntity2.Name);
        secondPageEntities.Should().Contain(e => e.Name == _testEntity3.Name);
    }
}
