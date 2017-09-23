using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;

namespace Rehearsal.Data
{
    public class EventReplayer
    {
        public EventReplayer(IEventPublisher eventPublisher)
        {
            EventPublisher = eventPublisher;
        }

        private IEventPublisher EventPublisher { get; }
        
        public Task ReplayEvents(IObservable<IEvent> events) => 
            events.Do(HandleEvent).ToTask();

        private void HandleEvent(IEvent @event)
        {
            EventPublisher.PublishEvent(@event, CancellationToken.None);
        }
    }
}