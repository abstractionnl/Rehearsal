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
            var questions = new[] {Faker.OpenRehearsalQuestion(), Faker.OpenRehearsalQuestion(), Faker.OpenRehearsalQuestion()};

            Given(() => new global::Rehearsal.Rehearsal.Rehearsal(Guid.NewGuid(), questions))
                .ThenEvent<RehearsalStartedEvent>(
                    @event => Check.That(@event.Questions).ContainsExactly(questions));
        }

        [Fact]
        public void CanCreateFromExisting()
        {
            var questions = new[] {Faker.OpenRehearsalQuestion(), Faker.OpenRehearsalQuestion(), Faker.OpenRehearsalQuestion()};

            var rehearsal = new global::Rehearsal.Rehearsal.Rehearsal(Guid.NewGuid(), questions);
            
            Given(() => global::Rehearsal.Rehearsal.Rehearsal.CreateFrom(Guid.NewGuid(), rehearsal))
                    .ThenEvent<RehearsalStartedEvent>(
                        @event => Check.That(@event.Questions).Contains(questions));
        }
    }
}