using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Authorization
{
    [EventName("UserCreatedEvent")]
    public class UserCreatedEvent : BaseEvent
    {
        public string Username { get; set; }
    }
}