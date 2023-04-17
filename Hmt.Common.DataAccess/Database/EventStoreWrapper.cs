using Hmt.Common.DataAccess.Interfaces;
using Marten.Events;

namespace Hmt.Common.DataAccess.Database;

public class EventStoreWrapper : IEventStoreWrapper
{
    private readonly IEventStore _eventStore;

    public EventStoreWrapper(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public void Append(Guid id, object @event)
    {
        _eventStore.Append(id, @event);
    }
}
