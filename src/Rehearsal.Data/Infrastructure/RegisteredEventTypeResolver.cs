using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LanguageExt;

namespace Rehearsal.Data.Infrastructure
{
    public class RegisteredEventTypeResolver : IEventTypeResolver
    {
        private IDictionary<string, Type> EventTypesByName { get; }
        private IDictionary<Type, string> EventNamesByType { get; }

        public RegisteredEventTypeResolver()
        {
            EventTypesByName = new Dictionary<string, Type>();
            EventNamesByType = new Dictionary<Type, string>();
        }

        public string TypeToString(Type type)
        {
            return EventNamesByType
                .TryGetValue(type)
                .IfNone(() => throw new InvalidOperationException($"Type {type.FullName} is not a registered event type"));
        }

        public Type StringToType(string typeName)
        {
            return EventTypesByName.TryGetValue(typeName)
                .IfNone(() => throw new InvalidOperationException($"Type {typeName} is not a registered event type"));
        }
        
        public void RegisterEventType(string typeName, Type type, bool asDefault = true)
        {
            if (EventTypesByName.ContainsKey(typeName))
                throw new InvalidOperationException($"Event type name {typeName} could not be registered, it allready registered for {EventTypesByName[typeName]}");

            if (asDefault)
            {
                if (EventNamesByType.ContainsKey(type))
                    throw new InvalidOperationException($"Event type {type.FullName} could not be registered as default, it is allready registered as {EventNamesByType[type]}");
                
                EventNamesByType[type] = typeName;
            }
            
            EventTypesByName[typeName] = type;
        }
    }
}