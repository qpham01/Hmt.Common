using FluentAssertions;
using Hmt.Common.DataAccess.Database;
using Hmt.Common.DataAccess.Interfaces;
using Moq;

namespace Hmt.Common.UnitTests.DataAccess
{
    [TestFixture]
    public class EntityStoreLongKeyTests
    {
        private Mock<IDocumentStoreWrapper<TestEntity, long>> _storeWrapperMock;
        private Mock<ISessionWrapper<TestEntity, long>> _sessionWrapperMock;
        private Mock<IEventStoreWrapper> _eventStoreWrapperMock;
        private EntityStoreLongKey<TestEntity> _entityStore;
        private Random rand = new();

        [SetUp]
        public void SetUp()
        {
            _storeWrapperMock = new Mock<IDocumentStoreWrapper<TestEntity, long>>();
            _eventStoreWrapperMock = new Mock<IEventStoreWrapper>();
            _sessionWrapperMock = new Mock<ISessionWrapper<TestEntity, long>>();
            _sessionWrapperMock.Setup(sw => sw.Events).Returns(_eventStoreWrapperMock.Object);
            _storeWrapperMock.Setup(sw => sw.OpenSession()).Returns(_sessionWrapperMock.Object);
            _entityStore = new EntityStoreLongKey<TestEntity>(_storeWrapperMock.Object);
        }

        [Test]
        public async Task CreateAsync_ShouldCallStoreAndSaveChanges()
        {
            var entity = new TestEntity { Id = GetRandomLong(), Name = "Test Entity" };

            await _entityStore.CreateAsync(entity);

            _sessionWrapperMock.Verify(sw => sw.Store(entity), Times.Once);
        }

        [Test]
        public async Task ReadAllAsync_ShouldReturnAllEntities()
        {
            var entities = new List<TestEntity>
            {
                new TestEntity
                {
                    Id = GetRandomLong(),
                    Name = "Test Entity 1",
                    IsDeleted = false
                },
                new TestEntity
                {
                    Id = GetRandomLong(),
                    Name = "Test Entity 2",
                    IsDeleted = false
                }
            };

            _sessionWrapperMock.Setup(sw => sw.QueryAll()).ReturnsAsync(entities);

            var result = await _entityStore.ReadAllAsync();

            _sessionWrapperMock.Verify(sw => sw.QueryAll(), Times.Once);
            result.Should().NotBeNull();
            result.Count().Should().Be(entities.Count);
        }

        [Test]
        public async Task ReadPageAsync_ShouldReturnPagedEntities()
        {
            var result = await _entityStore.ReadPageAsync(0, 4);
            _sessionWrapperMock.Verify(x => x.Query(), Times.Once);
            _sessionWrapperMock.Verify(x => x.CustomQuery(It.IsAny<IQueryable<TestEntity>>()), Times.Once);
        }

        [Test]
        public async Task ReadAsync_ShouldCallQueryAndReturnEntity()
        {
            var data = new List<TestEntity>().AsQueryable();
            _sessionWrapperMock.Setup(x => x.Query()).Returns(data);
            var result = await _entityStore.ReadAsync(101);
            _sessionWrapperMock.Verify(x => x.Query(), Times.Once);
            _sessionWrapperMock.Verify(x => x.CustomQuery(It.IsAny<IQueryable<TestEntity>>()), Times.Once);
        }

        [Test]
        public async Task SoftDeleteAsync_ShouldCallStoreAndSaveChanges()
        {
            _sessionWrapperMock
                .Setup(x => x.CustomQuery(It.IsAny<IQueryable<TestEntity>>()))
                .ReturnsAsync(new List<TestEntity> { new TestEntity() });
            await _entityStore.SoftDeleteAsync(101);
            _sessionWrapperMock.Verify(x => x.Query(), Times.Once);
            _sessionWrapperMock.Verify(x => x.CustomQuery(It.IsAny<IQueryable<TestEntity>>()), Times.Once);
            _sessionWrapperMock.Verify(x => x.Store(It.IsAny<TestEntity>()), Times.Once);
        }

        [Test]
        public async Task SoftDeleteAsync_NotFoundShouldNotStore()
        {
            await _entityStore.SoftDeleteAsync(101);
            _sessionWrapperMock.Verify(x => x.Query(), Times.Once);
            _sessionWrapperMock.Verify(x => x.CustomQuery(It.IsAny<IQueryable<TestEntity>>()), Times.Once);
            _sessionWrapperMock.Verify(x => x.Store(It.IsAny<TestEntity>()), Times.Never);
        }

        [Test]
        public async Task UpdateAsync_ShouldCallStoreAndSaveChanges()
        {
            var entity = new TestEntity { Id = GetRandomLong(), Name = "Test Entity" };

            await _entityStore.UpdateAsync(entity);

            _sessionWrapperMock.Verify(sw => sw.Store(entity), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_ShouldCallDeleteAsync()
        {
            var entity = new TestEntity { Id = GetRandomLong(), Name = "Test Entity" };

            await _entityStore.DeleteAsync(entity);

            _sessionWrapperMock.Verify(sw => sw.DeleteAsync(entity), Times.Once);
        }

        private long GetRandomLong()
        {
            return rand.Next(1, 100000);
        }

        public class TestEntity : IEntity<long>, ISoftDeletable, IDisposable
        {
            public long Id { get; set; }
            public string Name { get; set; } = string.Empty;
            public bool IsDeleted { get; set; }

            public void Dispose() { }
        }

        private class TestEvent
        {
            public long EventData { get; set; }
        }
    }
}
