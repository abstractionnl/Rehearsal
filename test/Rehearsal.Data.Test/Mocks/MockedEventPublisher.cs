using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;

namespace Rehearsal.Data.Test.Mocks
{
    public class MockedEventPublisher : IEventPublisher
    {
        public IList<IEvent> PublishedEvents { get; } = new List<IEvent>();
        
        public Task Publish<T>(T @event, CancellationToken cancellationToken = new CancellationToken()) where T : class, IEvent
        {
            PublishedEvents.Add(@event);
            return Task.CompletedTask;
        }
    }
}