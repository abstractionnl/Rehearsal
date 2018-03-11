using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Rehearsal.Data.Test.Mocks;
using Rehearsal.Messages.QuestionList;
using Rehearsal.Xunit;

namespace Rehearsal.Data.Test
{
    public static class FakerExtensions
    {
        public static SomeEvent SomeEvent(this Faker faker, Guid? entityId = null, int? version = null, DateTime? timeStamp = null) =>
            new SomeEvent()
            {
                Id = entityId ?? Guid.NewGuid(),
                Version = version ?? 1,
                TimeStamp = timeStamp ?? faker.Date.Recent(),
                SomeValue = faker.Lorem.Sentence()
            };

        public static SomeEvent[] SomeEventsFor(this Faker faker, Guid entityId, int count) =>
            Enumerable.Range(1, count)
                .Select(i => faker.SomeEvent(entityId, i, faker.Date.BetweenDaysAgo(i, i+1)))
                .ToArray();
        
        public static AnotherEvent AnotherEvent(this Faker faker, Guid? entityId = null, int? version = null, DateTime? timeStamp = null) =>
            new AnotherEvent()
            {
                Id = entityId ?? Guid.NewGuid(),
                Version = version ?? 1,
                TimeStamp = timeStamp ?? faker.Date.Recent(),
                AnotherValue = faker.Random.Number()
            };

        public static QuestionListModel QuestionListModel(this Faker faker) => new QuestionListModel
        {
            Id = Guid.NewGuid(),
            Title = faker.Lorem.Word(),
            QuestionTitle = faker.Lorem.Word(),
            AnswerTitle = faker.Lorem.Word(),
            Questions = new List<QuestionModel>
            {
                new QuestionModel() { Question = faker.Lorem.Word(), Answer = faker.Lorem.Word() },
                new QuestionModel() { Question = faker.Lorem.Word(), Answer = faker.Lorem.Word() }
            },
            Version = 1
        };

        public static QuestionListOverviewModel QuestionListOverviewModel(this Faker faker, string title = null) =>
            new QuestionListOverviewModel
            {
                Id = Guid.NewGuid(),
                Title = title ?? faker.Lorem.Word(),
                QuestionTitle = faker.Lorem.Word(),
                AnswerTitle = faker.Lorem.Word(),
                IsDeleted = false,
                QuestionsCount = 2
            };
    }
}