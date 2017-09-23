using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;

namespace Rehearsal.Data
{
    public static class EventPublisherExtensions
    {
        public static Task PublishEvent(this IEventPublisher eventPublisher, IEvent @event, CancellationToken cancellationToken)
        {
            return (Task)typeof(IEventPublisher).GetMethod(nameof(IEventPublisher.Publish))
                .MakeGenericMethod(@event.GetType())
                .Invoke(eventPublisher, new object[] { @event, cancellationToken });
        }
    }
}