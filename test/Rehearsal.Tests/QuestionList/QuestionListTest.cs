using System;
using NFluent;
using Rehearsal.Messages.QuestionList;
using Xunit;

namespace Rehearsal.Tests.QuestionList
{
    public class QuestionListTest : BaseAggregateTest<global::Rehearsal.QuestionList.QuestionList>
    {
        [Fact]
        public void CanCreate()
        {
            var properties = Faker.QuestionListProperties();
            
            Given(
                () => new global::Rehearsal.QuestionList.QuestionList(Guid.NewGuid(), properties)
            ).ThenEvent<QuestionListCreatedEvent>(
                @event => Check.That(@event.QuestionList).IsEqualTo(properties)
            );
        }

        [Fact]
        public void CanUpdate()
        {
            var properties = Faker.QuestionListProperties();

            Given(new QuestionListCreatedEvent { QuestionList = Faker.QuestionListProperties() })
                .When(q => q.Update(properties))
                .ThenEvent<QuestionListUpdatedEvent>(
                    @event => Check.That(@event.QuestionList).IsEqualTo(properties)
                );
        }

        [Fact]
        public void CanUpdateTwice()
        {
            var properties = Faker.QuestionListProperties();
            
            Given(
                new QuestionListCreatedEvent { QuestionList = Faker.QuestionListProperties() },
                new QuestionListUpdatedEvent { QuestionList = Faker.QuestionListProperties() }
            )
                .When(q => q.Update(properties))
                .ThenEvent<QuestionListUpdatedEvent>(
                    @event => Check.That(@event.QuestionList).IsEqualTo(properties)
                );
        }

        [Fact]
        public void CanDelete()
        {
            Given(new QuestionListCreatedEvent { QuestionList = Faker.QuestionListProperties() })
                .When(q => q.Delete())
                .ThenEvent<QuestionListDeletedEvent>();
        }

        [Fact]
        public void CannotDeleteTwice()
        {
            Given(
                new QuestionListCreatedEvent { QuestionList = Faker.QuestionListProperties() },
                new QuestionListDeletedEvent()
            ).ThrowsWhen<InvalidOperationException>(q => q.Delete());
        }
    }
}
