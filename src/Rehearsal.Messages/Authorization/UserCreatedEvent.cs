using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages.Authorization
{
    public class UserCreatedEvent : BaseEvent
    {
        public string Username { get; set; }
    }
}