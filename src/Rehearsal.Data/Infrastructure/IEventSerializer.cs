using CQRSlite.Events;

namespace Rehearsal.Data.Infrastructure
{
    public interface IEventSerializer
    {
        (string type, string data) Serialize(IEvent @event);
        IEvent Deserialize(string type, string data);
    }
}