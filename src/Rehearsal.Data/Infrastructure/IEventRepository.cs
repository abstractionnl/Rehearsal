using System;
using CQRSlite.Events;

namespace Rehearsal.Data.Infrastructure
{
    public interface IEventRepository : IEventStore
    {
        IObservable<IEvent> GetEventStream();
    }
}