using Rehearsal.Messages.Infrastructure;

namespace Rehearsal.Messages
{
    public class CreateUserCommand : BaseCommand
    {
        public string Username { get; set; }
    }
}