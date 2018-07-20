using System;

namespace Rehearsal.Messages.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class EventNameAttribute : Attribute
    {
        public EventNameAttribute(string eventName, params string[] alternateNames)
        {
            EventName = eventName;
            AlternateNames = alternateNames;
        }

        public string EventName { get; }
        public string[] AlternateNames { get; }
    }
}