using System;
using System.Collections.Generic;
using CQRSlite.Events;
using Rehearsal.Data.Infrastructure;

namespace Rehearsal.Data.Test.Mocks
{
    public class MockedEventSerializer : IEventSerializer
    {
        private readonly IDictionary<string, IEvent> _events = new Dictionary<string, IEvent>(); 
        
        public (string type, string data) Serialize(IEvent @event)
        {
            var eventId = Guid.NewGuid().ToString();
            _events[eventId] = @event;

            return (@event.GetType().Name, eventId);
        }

        public IEvent Deserialize(string type, string data)
        {
            return _events[data];
        }
    }
}