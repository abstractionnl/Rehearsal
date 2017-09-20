using System;
using CQRSlite.Events;
using LanguageExt;

namespace Rehearsal.Data.Test.Mocks
{
    public class AnotherEvent : Record<AnotherEvent>, IEvent
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public int AnotherValue { get; set; }
    }
}