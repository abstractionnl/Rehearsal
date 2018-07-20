using System;
using System.Globalization;
using System.IO;
using System.Text;
using CQRSlite.Events;
using Newtonsoft.Json;

namespace Rehearsal.Data.Infrastructure
{
    public class EventSerializer : IEventSerializer
    {
        public EventSerializer(JsonSerializer serializer) 
        {
            Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        private JsonSerializer Serializer { get; }
        
        public string Serialize(IEvent @event)
        {
            var sb = new StringBuilder(256);
            var sw = new StringWriter(sb, CultureInfo.InvariantCulture);
            using (var jsonWriter = new JsonTextWriter(sw))
            {
                Serializer.Serialize(jsonWriter, @event);
            }

            return sw.ToString();
        }
        
        public IEvent Deserialize(Type type, string data)
        {
            using (var reader = new JsonTextReader(new StringReader(data)))
            {
                return (IEvent)Serializer.Deserialize(reader, type);
            }
        }

        
    }
}