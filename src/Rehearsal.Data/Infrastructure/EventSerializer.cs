using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CQRSlite.Events;
using LanguageExt;
using Newtonsoft.Json;

namespace Rehearsal.Data.Infrastructure
{
    public class EventSerializer
    {
        public EventSerializer(JsonSerializer serializer, params Type[] eventTypes) 
        {
            Serializer = serializer;
            EventTypes = new Dictionary<string, Type>();

            foreach (var eventType in eventTypes) RegisterEventType(eventType);
        }

        private JsonSerializer Serializer { get; }
        private IDictionary<string, Type> EventTypes { get; }
        
        public (string type, string data) Serialize(IEvent @event)
        {
            EventTypes
                .TryGetValue(@event.GetType().Name)
                .Where(type => type == @event.GetType())
                .IfNone(() => throw new InvalidOperationException($"Type {@event.GetType().FullName} is not a registered event type"));
            
            var sb = new StringBuilder(256);
            var sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using (var jsonWriter = new JsonTextWriter(sw))
            {
                Serializer.Serialize(jsonWriter, @event);
            }

            return (type: @event.GetType().Name, data: sw.ToString());
        }
        
        public IEvent Deserialize(string type, string data)
        {
            var t = EventTypes.TryGetValue(type)
                .IfNone(() => throw new InvalidOperationException($"Type {type} is not a registered event type"));
            
            using (var reader = new JsonTextReader(new StringReader(data)))
            {
                return (IEvent)Serializer.Deserialize(reader, t);
            }
        }

        private void RegisterEventType(Type type) => EventTypes.Add(type.Name, type);
    }
}