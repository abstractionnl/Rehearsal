using System;
using System.Linq;
using System.Reflection;
using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Data.Infrastructure
{
    public static class RegisteredEventTypeResolverExtensions
    {
        public static void RegisterUnderTypeNameConvention(this RegisteredEventTypeResolver resolver, Type type)
        {
            resolver.RegisterEventType(type.Name, type);
        }

        public static void RegisterUnderAnnotatedEventNameConvention(this RegisteredEventTypeResolver resolver,
            Type type)
        {
            var attribute = type.GetCustomAttributes()
                .OfType<EventNameAttribute>()
                .HeadOrNone()
                .IfNone(() => throw new InvalidOperationException($"To register events the EventNameAttribute must be present on the event type, tried to register type: {type.FullName}"));
            
            resolver.RegisterEventType(attribute.EventName, type);

            foreach (var alternateName in attribute.AlternateNames)
            {
                resolver.RegisterEventType(alternateName, type, false);
            }
        }
    }
}