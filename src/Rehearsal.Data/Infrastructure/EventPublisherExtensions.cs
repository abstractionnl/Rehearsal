using System;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;

namespace Rehearsal.Data.Infrastructure
{
    public static class EventPublisherExtensions
    {
        internal static Task PublishEvent(this IEventPublisher eventPublisher, IEvent @event, CancellationToken cancellationToken)
        {
            return (Task)typeof(IEventPublisher).GetMethod(nameof(IEventPublisher.Publish))
                .MakeGenericMethod(@event.GetType())
                .Invoke(eventPublisher, new object[] { @event, cancellationToken });
        }
        
        public static Task ReplayEvents(this IEventPublisher eventPublisher, IObservable<IEvent> events) => 
            events
                .Select(e => Observable.FromAsync(cancellationToken => eventPublisher.PublishEvent(e, cancellationToken)))
                .Concat()
                .LastOrDefaultAsync()
                .ToTask();
    }
}