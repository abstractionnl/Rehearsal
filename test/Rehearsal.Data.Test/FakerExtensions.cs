using System;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using Rehearsal.Data.Test.Mocks;

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

        public static DateTime BetweenDaysAgo(this Date faker, int minimumDaysAgo, int maximumDaysAgo) => 
            faker.Between(DateTime.UtcNow.AddDays(maximumDaysAgo), DateTime.UtcNow.AddDays(minimumDaysAgo));
    }
}