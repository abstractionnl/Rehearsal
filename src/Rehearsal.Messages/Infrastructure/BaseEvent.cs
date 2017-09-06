using System;
using CQRSlite.Events;

namespace Rehearsal.Messages.Infrastructure
{
    public class BaseEvent : IEvent
    {
        public Guid Id { get; set; }

        public int Version { get; set; }

        public DateTimeOffset TimeStamp { get; set; }
    }
}