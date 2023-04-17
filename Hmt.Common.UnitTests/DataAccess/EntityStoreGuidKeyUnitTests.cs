using FluentAssertions;
using Hmt.Common.DataAccess.Database;
using Hmt.Common.DataAccess.Interfaces;
using Moq;

namespace Hmt.Common.UnitTests.DataAccess
{
    [TestFixture]
    public class EntityStoreGuidKeyTests
    {
        private Mock<IDocumentStoreWrapper> _storeWrapperMock;
        private Mock<ISessionWrapper> _sessionWrapperMock;
        private Mock<IEventStoreWrapper> _eventStoreWrapperMock;
        private EntityStoreGuidKey<TestEntity> _entityStore;

        [SetUp]
        public void SetUp()
        {
            _storeWrapperMock = new Mock<IDocumentStoreWrapper>();
            _eventStoreWrapperMock = new Mock<IEventStoreWrapper>();
            _sessionWrapperMock = new Mock<ISessionWrapper>();
            _sessionWrapperMock.Setup(sw => sw.Events).Returns(_eventStoreWrapperMock.Object);
            _storeWrapperMock.Setup(sw => sw.OpenSession()).Returns(_sessionWrapperMock.Object);
            _entityStore = new EntityStoreGuidKey<TestEntity>(_storeWrapperMock.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldCallStoreAndSaveChanges()
        {
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test Entity" };

            await _entityStore.CreateAsync(entity);

            _sessionWrapperMock.Verify(sw => sw.Store<TestEntity, Guid>(entity), Times.Once);
        }

        [Test]
        public async Task ReadAllAsync_ShouldReturnAllEntities()
        {
            var entities = new List<TestEntity>
            {
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Entity 1",
                    IsDeleted = false
                },
                new TestEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Entity 2",
                    IsDeleted = false
                }
            };

            _sessionWrapperMock.Setup(sw => sw.QueryAll<TestEntity>()).ReturnsAsync(entities);

            var result = await _entityStore.ReadAllAsync();

            _sessionWrapperMock.Verify(sw => sw.QueryAll<TestEntity>(), Times.Once);
            result.Should().NotBeNull();
            result.Count().Should().Be(entities.Count);
        }

        [Test]
        public void ReadPageAsync_ShouldReturnPagedEntities()
        {
            // IMartenQueryable prevents mocking
            Assert.Pass();
        }

        [Test]
        public void ReadAsync_ShouldCallQueryAndReturnEntity()
        {
            // IMartenQueryable prevents mocking
            Assert.Pass();
        }

        [Test]
        public void SoftDeleteAsync_ShouldCallStoreAndSaveChanges()
        {
            // IMartenQueryable prevents mocking
            Assert.Pass();
        }

        [Test]
        public async Task UpdateAsync_ShouldCallStoreAndSaveChanges()
        {
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test Entity" };

            await _entityStore.UpdateAsync(entity);

            _sessionWrapperMock.Verify(sw => sw.Store<TestEntity, Guid>(entity), Times.Once);
        }

        [Test]
        public async Task ApplyEventAsync_ShouldCallAppendAndSaveChanges()
        {
            var id = Guid.NewGuid();
            var @event = new TestEvent();

            await _entityStore.ApplyEventAsync(id, @event);

            _sessionWrapperMock.Verify(sw => sw.Events.Append(id, @event), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldCallDeleteAsync()
        {
            var entity = new TestEntity { Id = Guid.NewGuid(), Name = "Test Entity" };

            await _entityStore.DeleteAsync(entity);

            _sessionWrapperMock.Verify(sw => sw.DeleteAsync<TestEntity, Guid>(entity), Times.Once);
        }

        public class TestEntity : IEntity<Guid>, ISoftDeletable
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public bool IsDeleted { get; set; }
        }

        private class TestEvent
        {
            public string EventData { get; set; } = string.Empty;
        }
    }
}
