using System;
using NFluent;
using Rehearsal.Messages;
using Xunit;

namespace Rehearsal.Tests
{
    public class RehearsalTests : BaseAggregateTest<Rehearsal>
    {
        [Fact]
        public void CanCreate()
        {
            var questions = new[] {Faker.Question(), Faker.Question(), Faker.Question()};

            Given(() => new Rehearsal(Guid.NewGuid(), questions))
                .ThenEvent<RehearsalStartedEvent>(
                    @event => Check.That(@event.Questions).ContainsExactly(questions));
        }
    }
}