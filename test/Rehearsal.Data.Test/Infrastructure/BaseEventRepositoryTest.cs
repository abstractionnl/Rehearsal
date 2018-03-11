using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using Bogus;
using CQRSlite.Events;
using NFluent;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Data.Test.Mocks;
using Rehearsal.Xunit;
using Xunit;

namespace Rehearsal.Data.Test.Infrastructure
{
    public abstract class BaseEventRepositoryTest<TEventStore>
        where TEventStore: IEventRepository
    {
        protected readonly Faker Faker;
        protected TEventStore EventStore;
        protected readonly MockedEventPublisher EventPublisher;

        protected BaseEventRepositoryTest()
        {
            Faker = new Faker();
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
            var @event = Faker.SomeEvent();

            return TestEventStore(E(@event), @event.Id, E(@event));
        }
        
        [Fact]
        public Task OnlyReturnsEventForCorrectAggregate()
        {
            var @event = Faker.SomeEvent();
            var event2 = Faker.SomeEvent();

            return TestEventStore(E(@event, event2), @event.Id, E(@event));
        }
        
        [Fact]
        public Task ReturnsCorrectTypesWithMultipleEventTypes()
        {
            var aggregateId = Guid.NewGuid();
            var events = new IEvent[]
            {
                Faker.SomeEvent(aggregateId, 1),
                Faker.AnotherEvent(aggregateId, 2)
            };

            return TestEventStore(events, aggregateId, events);
        }

        [Fact]
        public Task OnlyReturnsEventsHigherThanSpecifiedVersion()
        {
            var aggregateId = Guid.NewGuid();
            var events = Faker.SomeEventsFor(aggregateId, 4);

            return TestEventStore(events, aggregateId, events.Skip(2), 2);
        }
        
        [Fact]
        public Task ReturnsEmptyListWhenNoEventsHaveHigherVersion()
        {
            var aggregateId = Guid.NewGuid();
            var events = Faker.SomeEventsFor(aggregateId, 2);

            return TestEventStore(events, aggregateId, new IEvent[0], 2);
        }

        [Fact]
        public async Task EventStorePublishesStoredEvents()
        {
            var events = E(Faker.SomeEvent(), Faker.AnotherEvent());

            await EventStore.Save(events);

            Check.That(EventPublisher.PublishedEvents).ContainsExactly(events);
        }

        [Fact]
        public async Task WhenNoEventsAreSavedEventStreamIsEmpty()
        {
            await EventStore.Save(E());
            var loadedEvents = await EventStore.GetEventStream().ToList().ToTask();

            Check.That(loadedEvents).IsEmpty();
        }
        
        [Fact]
        public async Task WhenOneEventIsSavedEventStreamContainsEvent()
        {
            var @event = Faker.SomeEvent();
            await EventStore.Save(E(@event));
            var loadedEvents = await EventStore.GetEventStream().ToList().ToTask();

            Check.That(loadedEvents).ContainsExactly(E(@event));
        }
        
        [Fact]
        public async Task WhenMultipleEventsAreSavedStreamContainsEvents()
        {
            var @events = Faker.SomeEventsFor(Guid.NewGuid(), 10);
            await EventStore.Save(events);
            var loadedEvents = await EventStore.GetEventStream().ToList().ToTask();

            Check.That(loadedEvents).ContainsExactly(events);
        }

        [Fact]
        public async Task EventsAreReturnedOrderedByTimestamp()
        {
            var event1 = Faker.SomeEvent(timeStamp: Faker.Date.BetweenDaysAgo(1, 2));
            var event2 = Faker.SomeEvent(timeStamp: Faker.Date.BetweenDaysAgo(10, 20));
            var event3 = Faker.SomeEvent(timeStamp: Faker.Date.BetweenDaysAgo(5, 10));
            
            await EventStore.Save(E(event1, event2, event3));
            
            var loadedEvents = await EventStore.GetEventStream().ToList().ToTask();
            Check.That(loadedEvents).ContainsExactly(E(event1, event3, event2));
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