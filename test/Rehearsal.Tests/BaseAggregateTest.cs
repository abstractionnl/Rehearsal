using System;
using System.Linq;
using Bogus;
using CQRSlite.Domain;
using CQRSlite.Events;
using NFluent;

namespace Rehearsal.Tests
{
    public abstract class BaseAggregateTest<TAggregate> : TestBase
        where TAggregate: AggregateRoot
    {
        protected AggregateTest Given(params IEvent[] events)
        {
            var id = Guid.NewGuid();
            var aggregate = (TAggregate)Activator.CreateInstance(typeof(TAggregate), nonPublic: true);

            aggregate.LoadFromHistory(events.Select(FillEvent(id)));
            
            return new AggregateTest(aggregate);
        }
        
        private Func<IEvent, int, IEvent> FillEvent(Guid id) =>
            (@event, i) =>
            {
                @event.Id = id;
                @event.Version = i + 1;
                @event.TimeStamp = DateTimeOffset.UtcNow.AddMinutes(-i);

                return @event;
            };

        protected AggregateTest Given(Func<TAggregate> construct)
        {
            var aggregate = construct();
            
            return new AggregateTest(aggregate);
        }

        protected class AggregateTest
        {
            public AggregateTest(TAggregate aggregate)
            {
                Aggregate = aggregate;
            }

            private TAggregate Aggregate { get; }

            public AggregateTest When(Action<TAggregate> action)
            {
                action(Aggregate);

                return this;
            }

            public AggregateTest ThrowsWhen<TException>(Action<TAggregate> action) 
                where TException : Exception
            {
                Check.ThatCode(() => action(Aggregate))
                    .Throws<TException>();

                return this;
            }

            public AggregateTest ThenEvent<TEvent>(params Action<TEvent>[] additionalAssertions)
                where TEvent: IEvent
            {
                Check.That(Aggregate.GetUncommittedChanges())
                    .ContainsInstanceOf<IEvent, TEvent>()
                    .Which.PerformAssertions(additionalAssertions);

                return this;
            }
        }
    }
}