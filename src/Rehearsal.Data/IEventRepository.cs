using System;
using CQRSlite.Events;

namespace Rehearsal.Data
{
    public interface IEventRepository : IEventStore
    {
        IObservable<IEvent> GetEventStream();
    }
}