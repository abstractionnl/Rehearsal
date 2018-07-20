using System;
using CQRSlite.Events;

namespace Rehearsal.Data.Infrastructure
{
    public interface IEventSerializer
    {
        string Serialize(IEvent @event);
        IEvent Deserialize(Type type, string data);
    }
}