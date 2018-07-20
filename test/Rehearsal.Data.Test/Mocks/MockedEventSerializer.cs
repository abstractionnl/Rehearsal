using System;
using System.Collections.Generic;
using CQRSlite.Events;
using Rehearsal.Data.Infrastructure;
using Xunit.Sdk;

namespace Rehearsal.Data.Test.Mocks
{
    public class MockedEventSerializer : IEventSerializer
    {
        private readonly IDictionary<string, IEvent> _events = new Dictionary<string, IEvent>(); 
        
        public string Serialize(IEvent @event)
        {
            var eventId = Guid.NewGuid().ToString();
            _events[eventId] = @event;

            return eventId;
        }

        public IEvent Deserialize(Type type, string data)
        {
            var @event = _events[data];
            
            if (type != @event.GetType())
                throw new AssertActualExpectedException(type, @event.GetType(), "type to deserialize does not match the type that was serialized");
            
            return @event;
        }
    }
}