using System;
using Bogus;
using Newtonsoft.Json;
using NFluent;
using Rehearsal.Data.Infrastructure;
using Rehearsal.Data.Test.Mocks;
using Xunit;

namespace Rehearsal.Data.Test.Infrastructure
{
    public class EventSerializerTest
    {
        protected readonly Faker Faker;
        
        public EventSerializerTest()
        {
            Faker = new Faker();
        }
        
        
        [Fact]
        public void CanSerializeAndDeserializeEvent()
        {
            var serializer = new EventSerializer(JsonSerializer.CreateDefault());
            var @event = Faker.SomeEvent();

            var data = serializer.Serialize(@event);

            Check.That(data).IsNotEmpty();

            var deserializedEvent = serializer.Deserialize(@event.GetType(), data);

            Check.That(deserializedEvent).IsEqualTo(@event);
            Check.That(deserializedEvent).Not.IsSameReferenceAs(@event);
        }
    }
}