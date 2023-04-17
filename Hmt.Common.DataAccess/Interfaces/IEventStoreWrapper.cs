namespace Hmt.Common.DataAccess.Interfaces;

public interface IEventStoreWrapper
{
    void Append(Guid id, object @event);
}
