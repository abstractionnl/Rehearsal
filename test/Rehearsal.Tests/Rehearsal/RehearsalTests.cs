using System;
using NFluent;
using Rehearsal.Messages.Rehearsal;
using Xunit;

namespace Rehearsal.Tests.Rehearsal
{
    public class RehearsalTests : BaseAggregateTest<global::Rehearsal.Rehearsal.Rehearsal>
    {
        [Fact]
        public void CanCreate()
        {
            var questions = new[] {Faker.Question(), Faker.Question(), Faker.Question()};

            Given(() => new global::Rehearsal.Rehearsal.Rehearsal(Guid.NewGuid(), questions))
                .ThenEvent<RehearsalStartedEvent>(
                    @event => Check.That(@event.Questions).ContainsExactly(questions));
        }
    }
}