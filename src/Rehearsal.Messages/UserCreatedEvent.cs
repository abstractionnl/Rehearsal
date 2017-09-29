using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages
{
    public class UserCreatedEvent : BaseEvent
    {
        public string Username { get; set; }
    }
}