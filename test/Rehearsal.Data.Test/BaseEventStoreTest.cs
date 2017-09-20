using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Bogus;
using CQRSlite.Events;
using NFluent;
using Rehearsal.Data.Test.Mocks;
using Xunit;

namespace Rehearsal.Data.Test
{
    public abstract class BaseEventStoreTest<TEventStore>
        where TEventStore: IEventStore
    {
        protected readonly Faker _faker;
        protected TEventStore EventStore;
        protected readonly MockedEventPublisher EventPublisher;

        public BaseEventStoreTest()
        {
            _faker = new Faker();
            EventPublisher = new MockedEventPublisher();
        }
        
        [Fact]
        public Task ReturnNothingForUnknownAggregrate()
        {
            return TestEventStore(E(), Guid.NewGuid(), E());
        }

        [Fact]
        public Task ReturnSavedEvent()
        {
            var @event = _faker.SomeEvent();

            return TestEventStore(E(@event), @event.Id, E(@event));
        }
        
        [Fact]
        public Task OnlyReturnsEventForCorrectAggregate()
        {
            var @event = _faker.SomeEvent();
            var event2 = _faker.SomeEvent();

            return TestEventStore(E(@event, event2), @event.Id, E(@event));
        }
        
        [Fact]
        public Task ReturnsCorrectTypesWithMultipleEventTypes()
        {
            var aggregateId = Guid.NewGuid();
            var events = new IEvent[]
            {
                _faker.SomeEvent(aggregateId, 1),
                _faker.AnotherEvent(aggregateId, 2)
            };

            return TestEventStore(events, aggregateId, events);
        }

        [Fact]
        public Task OnlyReturnsEventsHigherThanSpecifiedVersion()
        {
            var aggregateId = Guid.NewGuid();
            var events = _faker.SomeEventsFor(aggregateId, 4);

            return TestEventStore(events, aggregateId, events.Skip(2), 2);
        }
        
        [Fact]
        public Task ReturnsEmptyListWhenNoEventsHaveHigherVersion()
        {
            var aggregateId = Guid.NewGuid();
            var events = _faker.SomeEventsFor(aggregateId, 2);

            return TestEventStore(events, aggregateId, new IEvent[0], 2);
        }

        [Fact]
        public async Task EventStorePublishesStoredEvents()
        {
            var events = E(_faker.SomeEvent(), _faker.AnotherEvent());

            await EventStore.Save(events);

            Check.That(EventPublisher.PublishedEvents).ContainsExactly(events);
        }

        private static IEvent[] E(params IEvent[] events) => events;

        private async Task TestEventStore(IEnumerable<IEvent> eventsToSave, Guid aggregateId, IEnumerable<IEvent> expectedEvents, int fromVersion = 0)
        {
            await EventStore.Save(eventsToSave);
            var loadedEvents = await EventStore.Get(aggregateId, fromVersion);
            
            Check.That(loadedEvents).ContainsExactly(expectedEvents);
        }
    }
}